using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CafeApi.Application.Dtos.OrderDtos;
using CafeApi.Application.Dtos.ResponseDtos;
using CafeApi.Application.Interfaces;
using CafeApi.Application.Services.Abstract;
using CafeApi.Domain.Entities;
using FluentValidation;

namespace CafeApi.Application.Services.Concrete
{
    public class OrderItemService : IOrderItemServices
    {
        private readonly IGenericRepository<OrderItem> _genericRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderDto> _createOrderValidator;
        private readonly IValidator<UpdateOrderDto> _updateOrderValidator;



        public OrderItemService(IGenericRepository<OrderItem> genericRepository, IMapper mapper, IValidator<CreateOrderDto> createOrderValidator, IValidator<UpdateOrderDto> updateOrderValidator)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _createOrderValidator = createOrderValidator;
            _updateOrderValidator = updateOrderValidator;
        }

        public async Task<ResponseDto<List<ResultOrderDto>>> GetAllOrderItems()
        {
            try
            {
                var orderItems = await _genericRepository.GetAllAsync();
                if (orderItems.Count == 0)
                {
                    return new ResponseDto<List<ResultOrderDto>>
                    {
                        Success = false,
                        Data = null,
                        Message = "Siparis bulunamadi",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<List<ResultOrderDto>>(orderItems);
                return new ResponseDto<List<ResultOrderDto>>
                {
                    Success = true,
                    Data = result,
                    Message = "Siparisler listelendi..."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultOrderDto>>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir sorun olustu....",
                    ErrorCode = ErrorCodes.Exception

                };
            }


        }
        public async Task<ResponseDto<DetailOrderDto>> GetOrderItemById(int id)
        {
            try
            {
                var orderItem = await _genericRepository.GetByIdAsync(id);
                if (orderItem == null)
                {
                    return new ResponseDto<DetailOrderDto>
                    {
                        Success = false,
                        Data = null,
                        Message = "Siparis bulunamadi",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<DetailOrderDto>(orderItem);
                return new ResponseDto<DetailOrderDto>
                {
                    Success = true,
                    Data = result,
                    Message = "Siparis bulundu..."
                };

            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailOrderDto> 
                {
                    Success = false,
                    Data = null,
                    Message = "Bir sorun olustu....",
                    ErrorCode = ErrorCodes.Exception
                };
            }                

        }
        public async Task<ResponseDto<object>> AddOrderItem(CreateOrderDto dto)
        {
            try
            {
                var validationResult = await _createOrderValidator.ValidateAsync(dto);
                
                if(!validationResult.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Siparis eklenemedi...",
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }

                var result = _mapper.Map<OrderItem>(dto);
                if (result == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Siparis eklenemedi...",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await _genericRepository.AddAsync(result);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Siparis eklendi..."
                };

            }
            catch (Exception ex)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir sorun olustu....",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
      
        public async Task<ResponseDto<object>> DeleteOrderItem(int id)
        {
            try
            {
                var orderItem = await _genericRepository.GetByIdAsync(id);
                if (orderItem == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Siparis bulunamadi",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await _genericRepository.DeleteAsync(orderItem);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Siparis silindi..."
                };

            }
            catch (Exception ex)
            {

                return new  ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir sorun olustu....",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> UpdateOrderItem(UpdateOrderDto dto)
        {
            try
            {
                var validationResult = await _updateOrderValidator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Siparis guncellenemedi...",
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }

                var orderItem = await _genericRepository.GetByIdAsync(dto.Id);
                if (orderItem == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Siparis bulunamadi",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map(dto, orderItem);
                await _genericRepository.UpdateAsync(result);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Siparis guncellendi..."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir sorun olustu....",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
    }
    
}
