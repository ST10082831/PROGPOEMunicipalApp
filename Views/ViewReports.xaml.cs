using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using static POEPART1MunicipleApp.Models.ReportClasses;

namespace POEPART1MunicipleApp.Views
{
    public partial class ViewReports : UserControl
    {
        private Dictionary<int, Report> _reportsDictionary;

        public ViewReports(Dictionary<int, Report> reportsDictionary)
        {
            InitializeComponent();
            _reportsDictionary = reportsDictionary;
            PopulateReportsList(reportsDictionary);
        }

        private void PopulateReportsList(Dictionary<int, Report> reportsDictionary)
        {
            // Bind the ListView to the values of the dictionary (which are Report objects)
            ReportsListView.ItemsSource = new List<Report>(reportsDictionary.Values);
        }

        private void ViewDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var reportId = (int)button.CommandParameter;
            var mainWindow = Window.GetWindow(this) as MainWindow;

            if (_reportsDictionary.TryGetValue(reportId, out Report selectedReport))
            {
                mainWindow.MainContent.Content = new ViewReportDetails(selectedReport);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.MainContent.Content = new ReportIssues();
        }
    }
}
