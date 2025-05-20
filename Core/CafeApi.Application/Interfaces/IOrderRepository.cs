using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApi.Domain.Entities;

namespace CafeApi.Persistence.Repository
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrdersDetailsAsync();
        Task<Order> GetAllOrdersByIdWithDetailsAsync(int orderId);
    }
}
