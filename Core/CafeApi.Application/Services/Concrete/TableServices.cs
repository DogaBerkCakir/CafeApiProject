using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CafeApi.Application.Dtos.ResponseDtos;
using CafeApi.Application.Dtos.TableDtos;
using CafeApi.Application.Interfaces;
using CafeApi.Application.Services.Abstract;
using CafeApi.Application.Validators.Table;
using CafeApi.Domain.Entities;
using FluentValidation;

namespace CafeApi.Application.Services.Concrete
{
    
    public class TableServices : ITableServices
    {
        private readonly IGenericRepository<Table> _genericRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateTableDto> _validator;
        private readonly IValidator<UpdateTableDto> _updateTableValidator;
        private readonly ITableRepository _tableRepository;
        public TableServices(IGenericRepository<Table> genericRepository, IMapper mapper, IValidator<CreateTableDto> validator, IValidator<UpdateTableDto> updateTableValidator, ITableRepository tableRepository)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _validator = validator;
            _updateTableValidator = updateTableValidator;
            _tableRepository = tableRepository;
        }

        public async Task<ResponseDto<object>> AddTable(CreateTableDto createTableDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(createTableDto);
                if (!validationResult.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = string.Join(".", validationResult.Errors.Select(x => x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }
                var checkTable = await _genericRepository.GetByIdAsync(createTableDto.TableNumber);
                if (checkTable != null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Bu masa numarası zaten mevcut",
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }
                var table = _mapper.Map<Table>(createTableDto);
                await _genericRepository.AddAsync(table);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Masa Eklendi",
                    Data = table
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Bir Hata Oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> DeleteTable(int id)
        {
            try
            {
                var table = await _genericRepository.GetByIdAsync(id);
                if (table == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Masa Bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await _genericRepository.DeleteAsync(table);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Masa Silindi",
                    Data = table
                };

            }
            catch (Exception ex)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Bir Hata Oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }


        }

        public async Task<ResponseDto<List<ResultTableDto>>> GetAllActiveTablesGeneric() // yavas olan
        {
            try
            {
                var tables = await _genericRepository.GetAllAsync();
                tables = tables.Where(x => x.IsActive == true).ToList(); // bu sekilde de yapabiliriz ama yavas olur cunku once tum verileri cagırdık.....
                if (tables == null || tables.Count == 0)
                {
                    return new ResponseDto<List<ResultTableDto>>
                    {
                        Success = false,
                        Message = "Masa Bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<List<ResultTableDto>>(tables);
                return new ResponseDto<List<ResultTableDto>>
                {
                    Success = true,
                    Message = "Masalar Listesi",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultTableDto>>
                {
                    Success = false,
                    Message = "Bir Hata Oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<List<ResultTableDto>>> GetAllActiveTables()
        {
            try
            {
                var tables = await _tableRepository.GetAllActiveTablesAsync();
                if (tables == null || tables.Count == 0)
                {
                    return new ResponseDto<List<ResultTableDto>>
                    {
                        Success = false,
                        Message = "Masa Bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<List<ResultTableDto>>(tables);
                return new ResponseDto<List<ResultTableDto>>
                {
                    Success = true,
                    Message = "Masalar Listesi",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultTableDto>>
                {
                    Success = false,
                    Message = "Bir Hata Oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<List<ResultTableDto>>> GetAllTables()
        {
            try
            {
                var tables = await _genericRepository.GetAllAsync();
                if (tables == null || tables.Count == 0)
                {
                    return new ResponseDto<List<ResultTableDto>>
                    {
                        Success = false,
                        Message = "Masa Bulunamadı",
                        ErrorCode    = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<List<ResultTableDto>>(tables);
                return new ResponseDto<List<ResultTableDto>>
                {
                    Success = true,
                    Message = "Masalar Listesi",
                    Data = result
                };

            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultTableDto>>
                {
                    Success = false,
                    Message = "Bir Hata Oluştu",
                    ErrorCode = ErrorCodes.Exception
                };

            }
        }

        public async Task<ResponseDto<DetailTableDto>> GetByTableNumber(int tableNumber) //ayrı bir repository olusturmamız gerekecek generic repoda bu tanımlanmadı bu class a ozgu cunku
        {
            try
            {
                var table = await _tableRepository.GetByTableNumberAsync(tableNumber);
                if (table == null)
                {
                    return new ResponseDto<DetailTableDto>
                    {
                        Success = false,
                        Message = "Masa Bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<DetailTableDto>(table);
                return new ResponseDto<DetailTableDto>
                {
                    Success = true,
                    Message = "Masa Id ile bulundu...",
                    Data = result
                };

            }
            catch (Exception ex)
            {

                return new ResponseDto<DetailTableDto>
                {
                    Success = false,
                    Message = "Bir Hata Oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<DetailTableDto>> GetTableById(int id)
        {
            try
            {
                var table = await _genericRepository.GetByIdAsync(id);
                if (table == null)
                {
                    return new ResponseDto<DetailTableDto>
                    {
                        Success = false,
                        Message = "Masa Bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<DetailTableDto>(table);
                return new ResponseDto<DetailTableDto>
                {
                    Success = true,
                    Message = "Masa Id ile bulundu...",
                    Data = result
                };

            }
            catch (Exception ex)
            {

                return new ResponseDto<DetailTableDto>
                {
                    Success = false,
                    Message = "Bir Hata Oluştu",
                    ErrorCode = ErrorCodes.Exception
                }
                ;
            }
        }

        public async Task<ResponseDto<object>> UpdateTable(UpdateTableDto updateTableDto)
        {
            try
            {
                var validationResult = await _updateTableValidator.ValidateAsync(updateTableDto);
                if (!validationResult.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = string.Join(".", validationResult.Errors.Select(x => x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }

                var table = await _genericRepository.GetByIdAsync(updateTableDto.Id);
                var result = _mapper.Map(updateTableDto, table);
                await _genericRepository.UpdateAsync(result);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Masa Güncellendi",
                    Data = result
                };


            }
            catch (Exception ex)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Bir Hata Oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }

        }

        public async Task<ResponseDto<object>> UpdateTableStatusByNumber(int tableNumber)
        {
            try
            {
                var rs = await _tableRepository.GetByTableNumberAsync(tableNumber);
                if (rs == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Masa Bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }

                rs.IsActive = !rs.IsActive;
                await _genericRepository.UpdateAsync(rs);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = $"Masa Güncellendi ve {rs.IsActive} oldu..",
                    Data = rs
                };

            }
            catch (Exception ex)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Bir Hata Oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> UpdateTableStatusById(int tableId)
        {
            try
            {
                var rs = await _genericRepository.GetByIdAsync(tableId);
                if (rs == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Masa Bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }

                rs.IsActive = !rs.IsActive;
                await _genericRepository.UpdateAsync(rs);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = $"Masa Güncellendi ve {rs.IsActive} oldu..",
                    Data = rs
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Bir Hata Oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }





           
        }
    }
}
