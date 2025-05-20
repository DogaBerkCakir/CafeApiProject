using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApi.Domain.Entities;

namespace CafeApi.Application.Dtos.OrderItemDtos
{
    public class CreateOrderItemDto
    {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        //public decimal Price { get; set; }
    }
}
