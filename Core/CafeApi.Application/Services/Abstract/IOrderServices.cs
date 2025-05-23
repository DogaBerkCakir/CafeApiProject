﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApi.Application.Dtos.OrderDtos;
using CafeApi.Application.Dtos.ResponseDtos;

namespace CafeApi.Application.Services.Abstract
{
    public interface IOrderServices
    {
        Task<ResponseDto<List<ResultOrderDto>>> GetAllOrders();
        Task<ResponseDto<DetailOrderDto>> GetOrderById(int orderId);
        Task<ResponseDto<object>> AddOrder(CreateOrderDto dto);  
        Task<ResponseDto<object>> UpdateOrder(UpdateOrderDto dto);
        Task<ResponseDto<object>> DeleteOrder(int orderId);
        Task<ResponseDto<List<ResultOrderDto>>> GetAllOrderWithDetail();
        Task<ResponseDto<ResultOrderDto>> GetAllOrdersByIdWithDetails(int orderId);
        Task<ResponseDto<object>> UpdateOrderStatusHazir(int orderId);
        Task<ResponseDto<object>> UpdateOrderStatusHazirlaniyor(int orderId);
        Task<ResponseDto<object>> UpdateOrderStatusTeslimEdildi(int orderId);


    }
}
