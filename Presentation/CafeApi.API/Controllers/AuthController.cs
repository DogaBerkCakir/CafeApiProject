using CafeApi.Application.Dtos.AuthDtos;
using CafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthServices _authServices;

        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost("GenerateToken")]
        public async Task<IActionResult> GenerateToken(TokenDto dto)
        {
            var result = await _authServices.GenerateToken(dto);
            if(result == null)
            {
                return NotFound();
            }
            return CreateResponse(result);
        }




    }
}
