using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApi.Application.Dtos.OrderDtos;
using CafeApi.Application.Dtos.ResponseDtos;

namespace CafeApi.Application.Services.Abstract
{
    public interface IOrderItemServices
    {
        Task<ResponseDto<List<ResultOrderDto>>> GetAllOrderItems();
        Task<ResponseDto<DetailOrderDto>> GetOrderItemById(int id);
        Task<ResponseDto<object>> AddOrderItem(CreateOrderDto dto);
        Task<ResponseDto<object>> UpdateOrderItem(UpdateOrderDto dto);
        Task<ResponseDto<object>> DeleteOrderItem(int id);
    }
}
