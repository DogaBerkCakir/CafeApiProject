using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApi.Application.Dtos.ResponseDtos;
using CafeApi.Application.Dtos.TableDtos;

namespace CafeApi.Application.Services.Abstract
{
    public interface ITableServices
    {
        Task<ResponseDto<List<ResultTableDto>>> GetAllTables();
        Task<ResponseDto<List<ResultTableDto>>> GetAllActiveTablesGeneric();
        Task<ResponseDto<List<ResultTableDto>>> GetAllActiveTables();
        Task<ResponseDto<DetailTableDto>> GetTableById(int id);
        Task<ResponseDto<DetailTableDto>> GetByTableNumber(int tableNumber);
        Task<ResponseDto<object>> AddTable(CreateTableDto createTableDto);
        Task<ResponseDto<object>> UpdateTable(UpdateTableDto updateTableDto);
        Task<ResponseDto<object>> UpdateTableStatusByNumber(int tableNumber);
        Task<ResponseDto<object>> UpdateTableStatusById(int tableId);
        Task<ResponseDto<object>> DeleteTable(int id);
    }
}
