using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskB1.Domain.Interfaces.RepositoryInterfaces;

namespace TestTaskB1.Domain.Interfaces
{
    public interface IRepositoryManager
    {
        public IDataRecordRepository DataRecordRepository { get; }
        public IOcbRepository OcbRepository { get; }
        Task SaveAsync();
    }
}
