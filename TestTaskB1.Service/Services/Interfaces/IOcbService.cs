using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskB1.Domain.Models;

namespace TestTaskB1.Service.Services.Interfaces
{
    public interface IOcbService
    {
        void LoadExcelData(string filePath);
        void Save();
    }
}
