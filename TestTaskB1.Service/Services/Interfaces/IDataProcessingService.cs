using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskB1.Service.Services.Interfaces
{
    public interface IDataProcessingService
    {
        Task GenerateFilesAsync(string directoryPath, int fileCount = 100, int linesPerFile = 100000);
        Task<string> MergeFilesAsync(string[] filePaths, string outputFilePath, string substringToDelete);
        Task ImportDataAsync(string filePath);
        Task<(decimal sum, decimal median)> CalculateSumAndMedianAsync();
    }
}
