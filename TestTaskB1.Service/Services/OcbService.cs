using System.IO;
using TestTaskB1.Domain.Interfaces;
using TestTaskB1.Domain.Models;
using TestTaskB1.Service.Services.Interfaces;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Globalization;
using System.Diagnostics;

namespace TestTaskB1.Service.Services
{
    public class OcbService : IOcbService
    {
        private readonly IRepositoryManager _repository;

        public OcbService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public void LoadExcelData(string filePath)
        {
            // Сохранение информации о загруженном файле
            var fileUpload = new FileUploadModel
            {
                FileName = Path.GetFileName(filePath),
                UploadDate = DateTime.UtcNow
            };
            _repository.OcbRepository.AddFileUpload(fileUpload);
            Debug.WriteLine($"Uploaded file: {fileUpload.FileName} at {fileUpload.UploadDate}");

            using var file = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var workbook = new HSSFWorkbook(file);
            var worksheet = workbook.GetSheetAt(0);

            ClassModel currentClass = null;

            for (int row = 1; row <= worksheet.LastRowNum; row++)
            {
                var rowData = worksheet.GetRow(row);
                if (rowData == null || (rowData.GetCell(0) == null && rowData.GetCell(1) == null))
                {
                    Debug.WriteLine($"Row {row} is empty or has no data. Skipping.");
                    continue;
                }

                var className = rowData.GetCell(0)?.ToString();
                Debug.WriteLine($"Processing row {row}: ClassName = {className}");

                if (!string.IsNullOrEmpty(className) && !className.StartsWith("ПО КЛАССУ"))
                {
                    currentClass = new ClassModel
                    {
                        Name = className,
                        IncomingBalance = 0,
                        IncomingPassive = 0,
                        Debit = 0,
                        Credit = 0,
                        OutgoingBalance = 0,
                        OutgoingPassive = 0
                    };
                    _repository.OcbRepository.AddClass(currentClass);
                    Debug.WriteLine($"Added new Class: {currentClass.Name}");
                }

                if (currentClass != null)
                {
                    var accountNumber = rowData.GetCell(0)?.ToString(); 
                    var incomingBalance = GetDecimalValue(rowData.GetCell(1)); 
                    var incomingPassive = GetDecimalValue(rowData.GetCell(2)); 
                    var debit = GetDecimalValue(rowData.GetCell(3));
                    var credit = GetDecimalValue(rowData.GetCell(4));
                    var outgoingBalance = GetDecimalValue(rowData.GetCell(5));
                    var outgoingPassive = GetDecimalValue(rowData.GetCell(6)); 

                    var account = new Account
                    {
                        AccountNumber = accountNumber,
                        IncomingBalance = incomingBalance,
                        IncomingPassive = incomingPassive,
                        Debit = debit,
                        Credit = credit,
                        OutgoingBalance = outgoingBalance,
                        OutgoingPassive = outgoingPassive,
                        ClassId = currentClass.Id
                    };

                    currentClass.IncomingBalance += incomingBalance;
                    currentClass.IncomingPassive += incomingPassive;
                    currentClass.Debit += debit;
                    currentClass.Credit += credit;
                    currentClass.OutgoingBalance += outgoingBalance;
                    currentClass.OutgoingPassive += outgoingPassive;

                    _repository.OcbRepository.AddAccount(account);
                    Debug.WriteLine($"Added Account: {account.AccountNumber} for ClassId: {currentClass.Id}");
                }

                if (className.StartsWith("ПО КЛАССУ"))
                {
                    currentClass.IncomingBalance = GetDecimalValue(rowData.GetCell(1));
                    currentClass.IncomingPassive = GetDecimalValue(rowData.GetCell(2));
                    currentClass.Debit = GetDecimalValue(rowData.GetCell(3));
                    currentClass.Credit = GetDecimalValue(rowData.GetCell(4));
                    currentClass.OutgoingBalance = GetDecimalValue(rowData.GetCell(5));
                    currentClass.OutgoingPassive = GetDecimalValue(rowData.GetCell(6));
                    Debug.WriteLine($"Updated Class {currentClass.Name} with summary data.");
                }
            }
            Save();
        }

        private decimal GetDecimalValue(ICell cell)
        {
            if (cell == null)
                return 0;

            var value = cell.ToString()
                            .Replace(" ", "")
                            .Replace("\u00A0", "")
                            .Replace(",", ".");

            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }

            Debug.WriteLine($"Invalid decimal value: {value}");
            return 0;
        }

        public void Save()
        {
            _repository.SaveAsync().Wait();
            Debug.WriteLine("Data saved to the database.");
        }
    }
}