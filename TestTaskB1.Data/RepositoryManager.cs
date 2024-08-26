using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskB1.Data.DbContexts;
using TestTaskB1.Domain.Interfaces;
using TestTaskB1.Domain.Interfaces.RepositoryInterfaces;
using TestTaskB1.Data.Repositories;
namespace TestTaskB1.Data
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly Lazy<IDataRecordRepository> _dataRecordRepository;
        private readonly Lazy<IOcbRepository> _ocbRepository;
        public RepositoryManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _ocbRepository = new Lazy<IOcbRepository>(() => new OcbRepository(_dbContext));
            _dataRecordRepository = new Lazy<IDataRecordRepository>(() => new DataRecordRepository(_dbContext));
        }

        public IDataRecordRepository DataRecordRepository => _dataRecordRepository.Value;
        public IOcbRepository OcbRepository => _ocbRepository.Value;
        public async Task SaveAsync() => await _dbContext.SaveChangesAsync();
    }
}
