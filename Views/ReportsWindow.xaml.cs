using POEPART1MunicipalApp.Views;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using static POEPART1MunicipalApp.Models.ReportClasses;

namespace POEPART1MunicipalApp.Views
{
    public partial class ReportIssues : UserControl
    {
        // Dictionary to hold category IDs and names
        private Dictionary<int, string> categoryDictionary;
        // Reference to the main window to access shared data
        private MainWindow _mainWindow;
        // Path to the attached media file
        private string attachedMediaPath;

        public ReportIssues()
        {
            InitializeComponent();
            // Get the instance of MainWindow
            _mainWindow = (MainWindow)Application.Current.MainWindow;
            InitializeCategoryDictionary();
            PopulateCategoryComboBox();
        }

    
        // Initializes the category dictionary with predefined categories.
 
        private void InitializeCategoryDictionary()
        {
            categoryDictionary = new Dictionary<int, string>
            {
                { 1, "Sanitation" },
                { 2, "Roads" },
                { 3, "Utilities" },
                { 4, "Other" }
            };
        }

   
        // Populates the category ComboBox with available categories.

        private void PopulateCategoryComboBox()
        {
            CategoryComboBox.ItemsSource = categoryDictionary;
            CategoryComboBox.DisplayMemberPath = "Value";
            CategoryComboBox.SelectedValuePath = "Key";
        }

    
        // Handles the submit button click event to create and save a new report.
  
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate that all required fields are filled
            if (string.IsNullOrEmpty(LocationTextBox.Text) || CategoryComboBox.SelectedItem == null ||
                new TextRange(DescriptionRichTextBox.Document.ContentStart, DescriptionRichTextBox.Document.ContentEnd).Text.Trim().Length == 0)
            {
                MessageBox.Show("Please fill out all the fields before submitting.");
                return;
            }

            // Create a new report object with the provided information
            var newReport = new Report
            {
                Id = _mainWindow.ReportsDictionary.Count + 1,
                Location = LocationTextBox.Text,
                Category = ((KeyValuePair<int, string>)CategoryComboBox.SelectedItem).Value,
                Description = new TextRange(DescriptionRichTextBox.Document.ContentStart, DescriptionRichTextBox.Document.ContentEnd).Text.Trim(),
                MediaPath = attachedMediaPath
            };

            // Add the new report to the shared dictionary in MainWindow
            _mainWindow.ReportsDictionary.Add(newReport.Id, newReport);
            MessageBox.Show("Report submitted successfully!");
            ClearForm();
        }

        // Clears the form fields after a report is submitted.
       
        private void ClearForm()
        {
            LocationTextBox.Clear();
            CategoryComboBox.SelectedIndex = -1;
            DescriptionRichTextBox.Document.Blocks.Clear();
            attachedMediaPath = null;
        }

   
        // Handles the browse button click event to allow the user to attach media.
     
        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            // Open a file dialog to select an image file
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png|All Files|*.*"; // Allow image files
            bool? response = openFileDialog.ShowDialog();

            if (response == true)
            {
                attachedMediaPath = openFileDialog.FileName;
                MessageBox.Show($"Media attached: {attachedMediaPath}");
            }
            else
            {
                MessageBox.Show("No file selected.");
            }
        }

     
        // Navigates to the View Reports page.
     
        private void ViewReportsButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.NavigateToPage(new ViewReports(_mainWindow.ReportsDictionary));
        }

     
        // Navigates back to the main menu.
        private void BackToMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.ShowWelcomeMessage();  // Show welcome message on the main menu
            mainWindow.MainContent.Content = null; // Clear the content area
        }
    }
}
