using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using static POEPART1MunicipalApp.Models.ReportClasses;

namespace POEPART1MunicipalApp.Views
{
    public partial class ViewReports : UserControl
    {
        private Dictionary<int, Report> _reportsDictionary;

        public ViewReports(Dictionary<int, Report> reportsDictionary)
        {
            InitializeComponent();
            _reportsDictionary = reportsDictionary;
            DataContext = new List<Report>(_reportsDictionary.Values);
        }

        private void ViewDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var reportId = (int)button.CommandParameter;
            var mainWindow = Window.GetWindow(this) as MainWindow;

            if (_reportsDictionary.TryGetValue(reportId, out Report selectedReport))
            {
                mainWindow.NavigateToPage(new ViewReportDetails(selectedReport));
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.NavigateToPage(new ReportIssues());
        }
    }
}

