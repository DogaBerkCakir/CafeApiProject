using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApi.Application.Dtos.AuthDtos;
using CafeApi.Application.Dtos.ResponseDtos;
using CafeApi.Application.Helpers;
using CafeApi.Application.Services.Abstract;

namespace CafeApi.Application.Services.Concrete
{
    public class AuthServices : IAuthServices
    {
		private readonly TokenHelpers _tokenHelpers;

        public AuthServices(TokenHelpers tokenHelpers)
        {
            _tokenHelpers = tokenHelpers;
        }

        public async Task<ResponseDto<object>> GenerateToken(TokenDto dto)
        {
			try
			{
				var checkUser = dto.Email == "admin@admin.com" ? true : false;

                if (checkUser)
				{
                    var token = _tokenHelpers.GenerateToken(dto);
                    return new ResponseDto<object>
                    {
                        Success = true,
                        Data = token,
                        Message = "Token Olusturuldu...."
                    };
                }
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "Kullanıcı bulunamadı",
                    ErrorCode = ErrorCodes.Unauthorized
                };

            }
			catch (Exception ex)
			{

				return new ResponseDto<object>
				{
					Success = false,
					Data = null,
					Message = "bir hata olustu.....",
					ErrorCode = ErrorCodes.Exception
				};
			}
        }
    }
}
