using System.Collections.Generic;
using System.Linq;
using TestTaskB1.Domain.Models;
using TestTaskB1.Domain.Interfaces.RepositoryInterfaces;
using TestTaskB1.Data.DbContexts;

namespace TestTaskB1.Data.Repositories
{
    public class OcbRepository : IOcbRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OcbRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddClass(ClassModel classModel)
        {
            _dbContext.ClassModels.Add(classModel);
            _dbContext.SaveChanges();
        }

        public void AddAccount(Account accountModel)
        {
            _dbContext.Accounts.Add(accountModel);
            _dbContext.SaveChanges();
        }

        public void AddFileUpload(FileUploadModel fileUploadModel)
        {
            _dbContext.FileUploadModels.Add(fileUploadModel);
            _dbContext.SaveChanges();
        }

        public IEnumerable<ClassModel> GetAllClasses()
        {
            return _dbContext.ClassModels.ToList();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _dbContext.Accounts.ToList();
        }

        public IEnumerable<FileUploadModel> GetAllFileUploads()
        {
            return _dbContext.FileUploadModels.ToList();
        }
    }
}