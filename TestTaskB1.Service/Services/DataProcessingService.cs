using TestTaskB1.Domain.Interfaces;
using TestTaskB1.Domain.Models;
using TestTaskB1.Service.Services.Interfaces;

namespace TestTaskB1.Service.Services
{
    public class DataProcessingService : IDataProcessingService
    {
        private readonly IRepositoryManager _repositoryManager;

        public DataProcessingService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task GenerateFilesAsync(string directoryPath, int fileCount = 100, int linesPerFile = 100000)
        {
            var random = new Random();
            var tasks = new List<Task>();

            for (int i = 0; i < fileCount; i++)
            {
                var filePath = Path.Combine(directoryPath, $"file_{i + 1}.txt");
                tasks.Add(Task.Run(() =>
                {
                    using (var writer = new StreamWriter(filePath))
                    {
                        for (int j = 0; j < linesPerFile; j++)
                        {
                            var line = GenerateRandomLine(random);
                            writer.WriteLine(line);
                        }
                    }
                }));
            }

            await Task.WhenAll(tasks);
        }

        private string GenerateRandomLine(Random random)
        {
            var date = DateTime.UtcNow.AddDays(-random.Next(0, 1826)).ToString("dd.MM.yyyy");
            var latinChars = GenerateRandomString(random, 10, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");
            var cyrillicChars = GenerateRandomString(random, 10, "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстухцчшщъыьэюя");
            var evenNumber = random.Next(1, 50000001) * 2;
            var decimalNumber = Math.Round((decimal)(random.NextDouble() * 19 + 1), 8);

            return $"{date}||{latinChars}||{cyrillicChars}||{evenNumber}||{decimalNumber:0.########}||";
        }

        private string GenerateRandomString(Random random, int length, string chars)
        {
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<string> MergeFilesAsync(string[] filePaths, string outputFilePath, string substringToDelete)
        {
            var removedCount = 0;
            var mergedLines = new List<string>();

            // Проверяем, пустая ли подстрока
            bool isSubstringEmpty = string.IsNullOrWhiteSpace(substringToDelete);

            foreach (var filePath in filePaths)
            {
                var lines = await File.ReadAllLinesAsync(filePath);
                foreach (var line in lines)
                {
                    if (isSubstringEmpty || !line.Contains(substringToDelete))
                    {
                        mergedLines.Add(line);
                    }
                    else
                    {
                        removedCount++;
                    }
                }
            }

            await File.WriteAllLinesAsync(outputFilePath, mergedLines);
            return isSubstringEmpty
                ? "No lines removed as the substring was empty."
                : $"Removed {removedCount} lines containing '{substringToDelete}'.";
        }

        public async Task ImportDataAsync(string filePath)
        {
            var lines = await File.ReadAllLinesAsync(filePath);
            var records = new List<DataRecord>();

            for (int i = 0; i < lines.Length; i++)
            {
                var parts = lines[i].Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 5)
                {
                    var record = new DataRecord
                    {
                        RecordDate = DateTime.SpecifyKind(DateTime.Parse(parts[0]), DateTimeKind.Utc),
                        LatinCharacters = parts[1],
                        CyrillicCharacters = parts[2],
                        EvenInteger = int.Parse(parts[3]),
                        DecimalNumber = decimal.Parse(parts[4])
                    };
                    records.Add(record);
                }

                if (i % 1000 == 0)
                {
                    Console.WriteLine($"Imported {i} of {lines.Length} lines.");
                }
            }

            await _repositoryManager.DataRecordRepository.AddRangeAsync(records);
            Console.WriteLine("Import complete.");
        }

        public async Task<(decimal sum, decimal median)> CalculateSumAndMedianAsync()
        {
            var records = await _repositoryManager.DataRecordRepository.GetAllAsync();
            var sum = records.Sum(r => r.EvenInteger);
            var median = CalculateMedian(records.Select(r => r.DecimalNumber).ToList());
            return (sum, median);
        }

        private decimal CalculateMedian(List<decimal> numbers)
        {
            if (!numbers.Any()) return 0;

            numbers.Sort();
            var count = numbers.Count;
            if (count % 2 == 0)
            {
                return (numbers[count / 2 - 1] + numbers[count / 2]) / 2;
            }
            return numbers[count / 2];
        }
    }
}
