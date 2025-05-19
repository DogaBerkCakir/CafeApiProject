using CafeApi.Application.Dtos.ResponseDtos;
using CafeApi.Application.Dtos.TableDtos;
using CafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : BaseController
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
            return CreateResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTableById(int id)
        {
            var result = await _tableServices.GetTableById(id);
            return CreateResponse(result);
        }


        [HttpGet("getbytablenumber")]
        public async Task<IActionResult> GetByTableNumber(int tableNumber)
        {
            var result = await _tableServices.GetByTableNumber(tableNumber);
            return CreateResponse(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddTable(CreateTableDto createTableDto)
        {
            var result = await _tableServices.AddTable(createTableDto);
            return CreateResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTable(UpdateTableDto updateTableDto)
        {
            var result = await _tableServices.UpdateTable(updateTableDto);
            return CreateResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var result = await _tableServices.DeleteTable(id);
            return CreateResponse(result);
        }

        [HttpGet("getallactivetables")]
        public async Task<IActionResult> GetAllActiveTables()
        {
            var result = await _tableServices.GetAllActiveTables();
            return CreateResponse(result);
        }
        [HttpGet("getallactivetablesgeneric")]
        public async Task<IActionResult> GetAllActiveTablesGeneric()
        {
            var result = await _tableServices.GetAllActiveTablesGeneric();
            return CreateResponse(result);
        }

        [HttpPut("updatetablestatusbyid")]
        public async Task<IActionResult> UpdateTableStatusById(int tableId)
        {
            var result = await _tableServices.UpdateTableStatusById(tableId);
            return CreateResponse(result);
        }

        [HttpPut("updatetablestatusbynumber")]
        public async Task<IActionResult> UpdateTableStatusByNumber(int tableNumber)
        {
            var result = await _tableServices.UpdateTableStatusByNumber(tableNumber);
            return CreateResponse(result);
        }
    }
}
