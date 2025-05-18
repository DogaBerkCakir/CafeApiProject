using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApi.Application.Dtos.TableDtos;
using FluentValidation;

namespace CafeApi.Application.Validators.Table
{
    public class UpdateTableValidator : AbstractValidator<UpdateTableDto>
    {
        public UpdateTableValidator()
        {
            RuleFor(x => x.TableNumber)
                .NotEmpty()
                .WithMessage("Masa numarası boş olamaz.")
                .GreaterThan(0)
                .WithMessage("Masa numarası 0'dan büyük olmalıdır.");
            RuleFor(x => x.IsActive)
                .NotEmpty()
                .WithMessage("Masa boş mu dolu mu belirtilmeli.");
            RuleFor(x => x.Capacity)
                .NotEmpty()
                .WithMessage("Masa kapasitesi boş olamaz.")
                .GreaterThan(0)
                .WithMessage("Masa kapasitesi 0'dan büyük olmalıdır.")
                .LessThanOrEqualTo(20)
                .WithMessage("Masa kapasitesi 20'den büyük olamaz.");
        }
    }
    {
    }
}
