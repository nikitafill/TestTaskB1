using System.IO;
using System.Windows;
using Microsoft.Win32;
using TestTaskB1.Service.Services.Interfaces;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private readonly IOcbService _ocbService;
        private readonly IDataProcessingService _dataProcessingService;

        public MainWindow(IOcbService ocbService, IDataProcessingService dataProcessingService)
        {
            InitializeComponent();
            _ocbService = ocbService;
            _dataProcessingService = dataProcessingService;
        }

        private async void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            StatusTextBox.Text = "Generating files...";
            await _dataProcessingService.GenerateFilesAsync("D:\\Work\\B1\\WpfApp1\\Files", 100, 100000);
            StatusTextBox.Text = "Files generated.";
        }

        private async void MergeButton_Click(object sender, RoutedEventArgs e)
        {
            StatusTextBox.Text = "Merging files...";
            string directoryPath = "D:\\Work\\B1\\WpfApp1\\Files";
            string[] filePaths = Directory.GetFiles(directoryPath, "*.txt");
            string outputFilePath = Path.Combine(directoryPath, "merged.txt");

            string substringToRemove = SubstringTextBox.Text;

            string result = await _dataProcessingService.MergeFilesAsync(filePaths, outputFilePath, substringToRemove);
            StatusTextBox.Text = result;
        }

        private async void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            StatusTextBox.Text = "Importing data...";
            string filePath = @"D:\Work\B1\WpfApp1\Files\merged.txt"; // Укажите файл для импорта

            await _dataProcessingService.ImportDataAsync(filePath);
            StatusTextBox.Text = "Data imported.";
        }

        private void AddExcelButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx",
                Title = "Select an Excel File"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                StatusTextBox.Text = "Loading Excel data...";
                _ocbService.LoadExcelData(openFileDialog.FileName);
                StatusTextBox.Text = "Excel data loaded.";
            }
        }
    }
}