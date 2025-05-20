using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApi.Application.Dtos.OrderItemDtos;
using FluentValidation;

namespace CafeApi.Application.Validators.OrderItem
{
    public class AddOrderItemValidator : AbstractValidator<CreateOrderItemDto>
    {
        public AddOrderItemValidator()
        {
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithMessage("Siparis Adedi Bos Olamaz")
                .GreaterThan(0)
                .WithMessage("Siparis Adedi 0 dan buyuk olmalı");
        }
    }
    
}
