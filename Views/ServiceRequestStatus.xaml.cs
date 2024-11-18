using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using static POEPART1MunicipalApp.Models.ReportClasses;

namespace POEPART1MunicipalApp.Views
{
    public partial class ServiceRequestStatus : UserControl
    {
        private MainWindow _mainWindow; // Reference to the MainWindow for accessing shared data
        private Dictionary<string, string> statusTransitions; // Dictionary for managing status transitions

        public ServiceRequestStatus()
        {
            InitializeComponent(); // Initialize UI components from XAML
            _mainWindow = (MainWindow)Application.Current.MainWindow; // Reference to the main application window

            LoadAllServiceRequests(); // Load and display all service requests

            // Initialize the status transition graph (represents valid status changes)
            statusTransitions = new Dictionary<string, string>
            {
                { "Pending", "In Progress" }, // From Pending to In Progress
                { "In Progress", "Completed" }, // From In Progress to Completed
                { "Completed", "Pending" } // Circular transition example for demonstration
            };
        }

        // Method to load all service requests and display them in the list
        private void LoadAllServiceRequests()
        {
            Debug.WriteLine("[UI] Loading all service requests...");
            // Get all reports from the BST in sorted order
            var reports = _mainWindow.ReportsBST.InOrder();
            // Bind the reports to the list view for display
            ServiceRequestsList.ItemsSource = reports;
        }

        // Event handler for the search button to find a specific service request by ID
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Parse the entered text as an integer ID
            if (int.TryParse(SearchTextBox.Text, out int requestId))
            {
                Debug.WriteLine($"[UI] Searching for Report ID: {requestId}");
                // Search for the report in the BST using the ID
                var report = _mainWindow.ReportsBST.Search(requestId);
                if (report != null)
                {
                    Debug.WriteLine($"[UI] Found Report ID: {report.Id}");
                    // Display only the found report in the list
                    ServiceRequestsList.ItemsSource = new List<Report> { report };
                }
                else
                {
                    MessageBox.Show("Service Request not found."); // Display message if not found
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid Request ID."); // Handle invalid input
            }
        }

        // Event handler for the update status button to change the status of a report
        private void UpdateStatusButton_Click(object sender, RoutedEventArgs e)
        {
            // Extract the report ID from the button's CommandParameter
            var button = sender as Button;
            if (button != null && button.CommandParameter is int reportId)
            {
                Debug.WriteLine($"[UI] Updating Status for Report ID: {reportId}");
                // Search for the report in the BST using the ID
                var report = _mainWindow.ReportsBST.Search(reportId);
                if (report != null)
                {
                    // Get the next status for the current report's status
                    var nextStatus = GetNextStatus(report.Status);
                    if (nextStatus != null)
                    {
                        Debug.WriteLine($"[UI] Changing status from {report.Status} to {nextStatus}");
                        // Update the report's status
                        report.Status = nextStatus;
                        // Reload the list to reflect changes
                        LoadAllServiceRequests();
                    }
                    else
                    {
                        MessageBox.Show($"No valid transition for current status: {report.Status}"); // Handle invalid transitions
                    }
                }
                else
                {
                    MessageBox.Show("Report not found."); // Display message if the report is not found
                }
            }
            else
            {
                MessageBox.Show("Invalid operation."); // Handle invalid button actions
            }
        }

        // Method to get the next status for a given current status using the transition graph
        private string GetNextStatus(string currentStatus)
        {
            // Check if the current status has a valid transition in the dictionary
            if (statusTransitions.TryGetValue(currentStatus, out string nextStatus))
            {
                return nextStatus; // Return the next status
            }
            Debug.WriteLine($"[UI] No valid transition for status: {currentStatus}");
            return null; // Return null if no valid transition exists
        }
    }
}
