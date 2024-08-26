using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskB1.Domain.Models;

namespace TestTaskB1.Domain.Interfaces.RepositoryInterfaces
{
    public interface IOcbRepository
    {
        void AddClass(ClassModel classModel);
        void AddAccount(Account accountModel);
        void AddFileUpload(FileUploadModel fileUploadModel);
        IEnumerable<ClassModel> GetAllClasses();
        IEnumerable<Account> GetAllAccounts();
        IEnumerable<FileUploadModel> GetAllFileUploads();
    }
}
