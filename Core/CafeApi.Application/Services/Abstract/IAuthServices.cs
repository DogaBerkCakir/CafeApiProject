using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApi.Application.Dtos.AuthDtos;
using CafeApi.Application.Dtos.ResponseDtos;

namespace CafeApi.Application.Services.Abstract
{
    public interface IAuthServices
    {
        Task<ResponseDto<object>> GenerateToken(TokenDto dto);


    }
}
