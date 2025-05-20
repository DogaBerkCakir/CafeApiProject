using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApi.Domain.Entities;
using CafeApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CafeApi.Persistence.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetAllOrdersByIdWithDetailsAsync(int orderId)
        {
            var result = await _context.Orders
                .Where(x => x.Id == orderId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .ThenInclude(m => m.Category)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<Order>> GetAllOrdersDetailsAsync()
        {
            var result = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .ThenInclude(m => m.Category)
                .ToListAsync();

            return result;
        }
    }
}
