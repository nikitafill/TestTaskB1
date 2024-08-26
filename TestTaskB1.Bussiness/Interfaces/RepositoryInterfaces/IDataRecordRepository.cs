using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskB1.Domain.Models;

namespace TestTaskB1.Domain.Interfaces.RepositoryInterfaces
{
    public interface IDataRecordRepository
    {
        Task AddRangeAsync(IEnumerable<DataRecord> records);
        Task<IEnumerable<DataRecord>> GetAllAsync();
        Task<int> RemoveRecordsContainingAsync(string substring);
    }
}
