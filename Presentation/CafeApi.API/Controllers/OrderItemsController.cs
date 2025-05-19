﻿using CafeApi.Application.Dtos.OrderDtos;
using CafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : BaseController
    {
        private readonly IOrderItemServices _orderItemServices;
        public OrderItemsController(IOrderItemServices orderItemServices)
        {
            _orderItemServices = orderItemServices;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllOrderItems()
        {
            var result = await _orderItemServices.GetAllOrderItems();
            return CreateResponse(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderItemById(int id)
        {
            var result = await _orderItemServices.GetOrderItemById(id);
            return CreateResponse(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrderItem(CreateOrderDto dto)
        {
            var result = await _orderItemServices.AddOrderItem(dto);
            return CreateResponse(result);
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var result = await _orderItemServices.DeleteOrderItem(id);
            return CreateResponse(result);
        }



    }
}
