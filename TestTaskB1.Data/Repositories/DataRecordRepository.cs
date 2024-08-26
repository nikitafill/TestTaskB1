using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskB1.Data.DbContexts;
using TestTaskB1.Domain.Models;
using TestTaskB1.Domain.Interfaces.RepositoryInterfaces;
namespace TestTaskB1.Data.Repositories
{
    public class DataRecordRepository : IDataRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public DataRecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<DataRecord> records)
        {
            await _context.DataRecords.AddRangeAsync(records);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DataRecord>> GetAllAsync()
        {
            return await _context.DataRecords.ToListAsync();
        }

        public async Task<int> RemoveRecordsContainingAsync(string substring)
        {
            var recordsToRemove = await _context.DataRecords
                .Where(r => r.LatinCharacters.Contains(substring) || r.CyrillicCharacters.Contains(substring))
                .ToListAsync();

            _context.DataRecords.RemoveRange(recordsToRemove);
            return await _context.SaveChangesAsync();
        }
    }
}