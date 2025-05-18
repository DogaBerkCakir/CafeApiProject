using CafeApi.Application.Dtos.ResponseDtos;
using CafeApi.Application.Dtos.TableDtos;
using CafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly ITableServices _tableServices;
        public TablesController(ITableServices tableServices)
        {
            _tableServices = tableServices;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllTables()
        {
            var result = await _tableServices.GetAllTables();
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTableById(int id)
        {
            var result = await _tableServices.GetTableById(id);
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpGet("getbytablenumber")]
        public async Task<IActionResult> GetByTableNumber(int tableNumber)
        {
            var result = await _tableServices.GetByTableNumber(tableNumber);
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddTable(CreateTableDto createTableDto)
        {
            var result = await _tableServices.AddTable(createTableDto);
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.ValidationError)
                {
                    return BadRequest(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTable(UpdateTableDto updateTableDto)
        {
            var result = await _tableServices.UpdateTable(updateTableDto);
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.ValidationError)
                {
                    return BadRequest(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var result = await _tableServices.DeleteTable(id);
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
