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
using FluentValidation;

namespace CafeApi.Application.Services.Concrete
{
    public class OrderItemService : IOrderItemServices
    {
        private readonly IGenericRepository<OrderItem> _orderItemRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderItemDto> _createOrderItemValidator;
        private readonly IValidator<UpdateOrderItemDto> _updateOrderItemValidator;
        private readonly IGenericRepository<MenuItem> _menuItemRepository;

        public OrderItemService(IGenericRepository<OrderItem> orderItemRepository, IMapper mapper, IValidator<CreateOrderItemDto> createOrderItemValidator, IValidator<UpdateOrderItemDto> updateOrderItemValidator, IGenericRepository<MenuItem> menuItemRepository)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
            _createOrderItemValidator = createOrderItemValidator;
            _updateOrderItemValidator = updateOrderItemValidator;
            _menuItemRepository = menuItemRepository;
        }


        public async Task<ResponseDto<object>> AddOrderItem(CreateOrderItemDto dto)
        {
            try
            {
                var validate = await _createOrderItemValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = string.Join(" | ", validate.Errors.Select(x => x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError,
                        Data = null
                    };
                }
                var orderItem = _mapper.Map<OrderItem>(dto);
                await _orderItemRepository.AddAsync(orderItem);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Sipariş Eklendi",
                    Data = orderItem
                };

            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Bir Hata Oluştu...",
                    ErrorCode = ErrorCodes.Exception
                };

            }
        }

        public async Task<ResponseDto<object>> DeleteOrderItem(int id)
        {
            try
            {
                var orderItem = await _orderItemRepository.GetByIdAsync(id);
                if (orderItem == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Sipariş Bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await _orderItemRepository.DeleteAsync(orderItem);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Sipariş Silindi",
                    Data = null
                };

            }
            catch (Exception ex)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Bir Hata Oluştu...",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<List<ResultOrderItemDto>>> GetAllOrderItems()
        {
            try
            {
                var result = await _orderItemRepository.GetAllAsync();
                var menuItems = await _menuItemRepository.GetAllAsync();
                if (result.Count == 0)
                {
                    return new ResponseDto<List<ResultOrderItemDto>>
                    {
                        Success = false,
                        Message = "Sipariş Bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }

                var mappedResult = _mapper.Map<List<ResultOrderItemDto>>(result);
                return new ResponseDto<List<ResultOrderItemDto>>
                {
                    Success = true,
                    Message = "Siparişler Listelendi",
                    Data = mappedResult
                };

            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultOrderItemDto>>
                {
                    Success = false,
                    Message = "Bir Hata Oluştu...",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<DetailOrderItemDto>> GetOrderItemById(int id)
        {
            try
            {
                var orderItem = await _orderItemRepository.GetByIdAsync(id);
                if (orderItem == null)
                {
                    return new ResponseDto<DetailOrderItemDto>
                    {
                        Success = false,
                        Message = "Sipariş Bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var mappedResult = _mapper.Map<DetailOrderItemDto>(orderItem);
                return new ResponseDto<DetailOrderItemDto>
                {
                    Success = true,
                    Message = "Sipariş Detayı",
                    Data = mappedResult
                };

            }
            catch (Exception ex)
            {

                return new ResponseDto<DetailOrderItemDto>
                {
                    Success = false,
                    Message = "Bir Hata Oluştu...",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> UpdateOrderItem(UpdateOrderItemDto dto)
        {
            try
            {
                var validate = await _updateOrderItemValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = string.Join(" | ", validate.Errors.Select(x => x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError,
                        Data = null
                    };
                }
                var orderItem = await _orderItemRepository.GetByIdAsync(dto.Id);
                if (orderItem == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Sipariş Bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var mappedResult = _mapper.Map(dto, orderItem);
                await _orderItemRepository.UpdateAsync(mappedResult);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Sipariş Güncellendi",
                    Data = mappedResult
                };


            }
            catch (Exception ex)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Bir Hata Oluştu...",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
    }
    
}
