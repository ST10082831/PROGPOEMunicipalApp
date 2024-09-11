using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using static POEPART1MunicipleApp.Models.ReportClasses;

namespace POEPART1MunicipleApp.Views
{
    public partial class ViewReportDetails : UserControl
    {
        private Report _selectedReport;

        public ViewReportDetails(Report report)
        {
            InitializeComponent();
            _selectedReport = report;
            DisplayReportDetails();
        }

        private void DisplayReportDetails()
        {
            // Display basic report details
            ReportIdTextBlock.Text = _selectedReport.Id.ToString();
            LocationTextBlock.Text = _selectedReport.Location;
            CategoryTextBlock.Text = _selectedReport.Category;
            DescriptionTextBlock.Text = _selectedReport.Description;

            // Display attached image if available
            if (!string.IsNullOrEmpty(_selectedReport.MediaPath) && File.Exists(_selectedReport.MediaPath))
            {
                try
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(_selectedReport.MediaPath);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    AttachedImage.Source = bitmap; // Set the Image control's source
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}");
                }
            }
            else
            {
                AttachedImage.Source = null; // No image available
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.MainContent.Content = new ViewReports(mainWindow.ReportsDictionary);
        }
    }
}
