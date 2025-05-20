using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApi.Application.Dtos.OrderDtos;
using FluentValidation;

namespace CafeApi.Application.Validators.Order
{
   public class UpdateOrderValidator : AbstractValidator<UpdateOrderDto>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.TotalPrice).NotEmpty().WithMessage("Bos gönderemezsin")
                .GreaterThan(0).WithMessage("0 dan buyuk olmalı..");
        }
    }
    
}
