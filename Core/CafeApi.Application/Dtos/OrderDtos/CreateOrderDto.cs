using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApi.Application.Dtos.OrderItemDtos;
using CafeApi.Domain.Entities;

namespace CafeApi.Application.Dtos.OrderDtos
{
    public class CreateOrderDto
    {
        public int TableId { get; set; }
        //public decimal TotalPrice { get; set; }
        //public DateTime CreatedAt { get; set; } = DateTime.Now;
        //public DateTime? UpdateAt { get; set; }
        //public string Status { get; set; } // buraya biz servisten gönderim saglayacagız
        public List<CreateOrderItemDto> OrderItems { get; set; }

    }
}
