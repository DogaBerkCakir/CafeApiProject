using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeApi.Application.Dtos.ResponseDtos;
using CafeApi.Application.Interfaces;
using CafeApi.Domain.Entities;
using CafeApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CafeApi.Persistence.Repository
{
    public class TableRepository : ITableRepository
    {
        private readonly AppDbContext _context;
        public TableRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Table>> GetAllActiveTablesAsync()
        {
            var result = await _context.Tables
                .Where(x => x.IsActive == true)
                .ToListAsync();
            return result;
        }

        public async Task<Table> GetByTableNumberAsync(int tableNumber)
        {
            var result = await _context.Tables.FirstOrDefaultAsync(x => x.TableNumber == tableNumber);
            return result;
        }

    }
}
