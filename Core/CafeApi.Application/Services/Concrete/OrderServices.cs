using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CafeApi.Application.Dtos.OrderDtos;
using CafeApi.Application.Dtos.OrderItemDtos;
using CafeApi.Application.Dtos.ResponseDtos;
using CafeApi.Application.Interfaces;
using CafeApi.Application.Services.Abstract;
using CafeApi.Domain.Entities;
using CafeApi.Persistence.Repository;
using FluentValidation;

namespace CafeApi.Application.Services.Concrete
{
    public class OrderServices : IOrderServices
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderDto> _createOrderValidator;
        private readonly IValidator<UpdateOrderDto> _updateOrderValidator;
        private readonly IGenericRepository<OrderItem> _orderItemRepository;
        private readonly IGenericRepository<MenuItem> _menuItemRepository;
        private readonly IOrderRepository _orderRepository1;
        public OrderServices(IGenericRepository<Order> orderRepository, IMapper mapper, IValidator<CreateOrderDto> validator, IValidator<UpdateOrderDto> updateOrderValidator, IGenericRepository<OrderItem> orderItemRepository, IGenericRepository<MenuItem> menuItemRepository, IOrderRepository orderRepository1)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _createOrderValidator = validator;
            _updateOrderValidator = updateOrderValidator;
            _orderItemRepository = orderItemRepository;
            _menuItemRepository = menuItemRepository;
            _orderRepository1 = orderRepository1;
        }

        public async Task<ResponseDto<object>> AddOrder(CreateOrderDto dto)
        {
            try
            {
                var valid = await _createOrderValidator.ValidateAsync(dto);
                if (!valid.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Success = false,
                        Message = "Sipariş eklenemedi.",
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }

                var order = _mapper.Map<Order>(dto);

                order.Status = OrderStatus.Hazir;
                order.CreatedAt = DateTime.Now;
                decimal totalPrice = 0;

                foreach (var item in order.OrderItems)
                {
                    item.MenuItem = await _menuItemRepository.GetByIdAsync(item.MenuItemId);
                    item.Price = item.MenuItem.Price * item.Quantity;
                    totalPrice += item.Price;
                }
                order.TotalPrice = totalPrice;  

                await _orderRepository.AddAsync(order);
                return new ResponseDto<object>
                {
                    Data = null,
                    Success = true,
                    Message = "Sipariş başarıyla eklendi.",
                    ErrorCode = null
                };



            }
            catch (Exception ex )
            {

                return new ResponseDto<object>
                {
                    Data = null,
                    Success = false,
                    Message = "Bir hata oluştu...",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> DeleteOrder(int orderId)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(orderId);
                if (order == null)
                {
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Success = false,
                        Message = "Sipariş bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await _orderRepository.DeleteAsync(order);
                return new ResponseDto<object>
                {
                    Data = null,
                    Success = true,
                    Message = "Sipariş başarıyla silindi.",
                    ErrorCode = null
                };

            }
            catch (Exception ex)
            {

                return new ResponseDto<object>
                {
                    Data = null,
                    Success = false,
                    Message = "Bir hata oluştu...",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<List<ResultOrderDto>>> GetAllOrders()
        {
            try
            {
                var orders = await _orderRepository.GetAllAsync();
                var orderItems = await _orderItemRepository.GetAllAsync();
                var menuItems = await _menuItemRepository.GetAllAsync();

                if (orders.Count == 0 || orders == null)
                {
                    return new ResponseDto<List<ResultOrderDto>>
                    {
                        Data = null,
                        Success = false,
                        Message = "Siparişler bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<List<ResultOrderDto>>(orders );
                return new ResponseDto<List<ResultOrderDto>>
                {
                    Data = result,
                    Success = true,
                    Message = "Siparişler başarıyla listelendi.",
                    ErrorCode = null
                };

            }
            catch (Exception ex)
            {

                return new ResponseDto<List<ResultOrderDto>>
                {
                    Data = null,
                    Success = false,
                    Message = "Bir hata oluştu...",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<ResultOrderDto>> GetAllOrdersByIdWithDetails(int orderId)
        {
            try
            {
                var result = await _orderRepository1.GetAllOrdersByIdWithDetailsAsync(orderId);
                if (result == null)
                {
                    return new ResponseDto<ResultOrderDto>
                    {
                        Data = null,
                        Success = false,
                        Message = "Sipariş bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var mappedResult = _mapper.Map<ResultOrderDto>(result);
                return new ResponseDto<ResultOrderDto>
                {
                    Data = mappedResult,
                    Success = true,
                    Message = "Sipariş başarıyla listelendi.",
                    ErrorCode = null
                };

            }
            catch (Exception ex)
            {

                return new ResponseDto<ResultOrderDto>              {
                    Data = null,
                    Success = false,
                    Message = "Bir hata oluştu... ",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
            
        public async Task<ResponseDto<List<ResultOrderDto>>> GetAllOrderWithDetail()
        {
            try
            {
                var result = await _orderRepository1.GetAllOrdersDetailsAsync();
                if (result == null)
                {
                    return new ResponseDto<List<ResultOrderDto>>
                    {
                        Data = null,
                        Success = false,
                        Message = "Siparişler bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var mappedResult = _mapper.Map<List<ResultOrderDto>>(result);
                return new ResponseDto<List<ResultOrderDto>>
                {
                    Data = mappedResult,
                    Success = true,
                    Message = "Siparişler başarıyla listelendi.",
                    ErrorCode = null
                };

            }
            catch (Exception ex)
            {

                return new ResponseDto<List<ResultOrderDto>>
                {
                    Data = null,
                    Success = false,
                    Message = "Bir hata oluştu...",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<DetailOrderDto>> GetOrderById(int orderId)
        {
            try
            {
               var result = await _orderRepository.GetByIdAsync(orderId);
                if (result == null)
                {
                    return new ResponseDto<DetailOrderDto>
                    {
                        Data = null,
                        Success = false,
                        Message = "Sipariş bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }

                var order = _mapper.Map<DetailOrderDto>(result);
                return new ResponseDto<DetailOrderDto>
                {
                    Data = order,
                    Success = true,
                    Message = "Sipariş başarıyla listelendi.",
                    ErrorCode = null
                };


            }
            catch (Exception ex)
            {

                return new ResponseDto<DetailOrderDto>
                {
                    Data = null,
                    Success = false,
                    Message = "Bir hata oluştu...",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> UpdateOrder(UpdateOrderDto dto)
        {
            try
            {
                var valid = await _updateOrderValidator.ValidateAsync(dto);
                if (!valid.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Success = false,
                        Message = "Sipariş güncellenemedi.",
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }

                var order = await _orderRepository.GetByIdAsync(dto.Id);
                if (order == null)
                {
                    return new ResponseDto<object>
                    {
                        Data = null,
                        Success = false,
                        Message = "Sipariş bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map(dto, order);
                await _orderRepository.UpdateAsync(result);
                return new ResponseDto<object>
                {
                    Data = null,
                    Success = true,
                    Message = "Sipariş başarıyla güncellendi.",
                    ErrorCode = null
                };



            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Data = null,
                    Success = false,
                    Message = "Bir hata oluştu...",
                    ErrorCode = ErrorCodes.Exception
                };

            }
        }
    }
}
