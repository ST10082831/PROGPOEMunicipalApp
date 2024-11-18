using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using static POEPART1MunicipalApp.Models.ReportClasses;

namespace POEPART1MunicipalApp.Views
{
    public partial class ViewReports : UserControl
    {
        // Dictionary to store all reports, keyed by their unique IDs
        private Dictionary<int, Report> _reportsDictionary;

        public ViewReports(Dictionary<int, Report> reportsDictionary)
        {
            InitializeComponent(); // Initialize the UI components defined in the XAML file
            _reportsDictionary = reportsDictionary; // Assign the passed dictionary to the private field

            // Set the DataContext to a list of reports extracted from the dictionary
            // This binds the data to the UI, allowing the reports to be displayed
            DataContext = new List<Report>(_reportsDictionary.Values);
        }

        // Event handler for the "View Details" button click
        private void ViewDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the button that was clicked and extract the associated report ID
            var button = sender as Button;
            var reportId = (int)button.CommandParameter;

            // Get a reference to the main window to navigate between pages
            var mainWindow = Window.GetWindow(this) as MainWindow;

            // Check if the report ID exists in the dictionary
            if (_reportsDictionary.TryGetValue(reportId, out Report selectedReport))
            {
                // Navigate to the ViewReportDetails page, passing the selected report
                mainWindow.NavigateToPage(new ViewReportDetails(selectedReport));
            }
        }

        // Event handler for the "Back" button click
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Get a reference to the main window
            var mainWindow = Window.GetWindow(this) as MainWindow;

            // Navigate back to the ReportIssues page
            mainWindow.NavigateToPage(new ReportIssues());
        }
    }
}
