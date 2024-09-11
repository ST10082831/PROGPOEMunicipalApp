using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using static POEPART1MunicipleApp.Models.ReportClasses;

namespace POEPART1MunicipleApp.Views
{
    public partial class ReportIssues : UserControl
    {
        private Dictionary<int, string> categoryDictionary;
        private MainWindow _mainWindow; // Reference to MainWindow for shared dictionary
        private string attachedMediaPath;

        public ReportIssues()
        {
            InitializeComponent();
            _mainWindow = (MainWindow)Application.Current.MainWindow; // Access the MainWindow to share reportsDictionary
            InitializeCategoryDictionary();
            PopulateCategoryComboBox();
        }

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

        private void PopulateCategoryComboBox()
        {
            CategoryComboBox.ItemsSource = categoryDictionary;
            CategoryComboBox.DisplayMemberPath = "Value";
            CategoryComboBox.SelectedValuePath = "Key";
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LocationTextBox.Text) || CategoryComboBox.SelectedItem == null ||
                new TextRange(DescriptionRichTextBox.Document.ContentStart, DescriptionRichTextBox.Document.ContentEnd).Text.Trim().Length == 0)
            {
                MessageBox.Show("Please fill out all the fields before submitting.");
                return;
            }

            var newReport = new Report
            {
                Id = _mainWindow.ReportsDictionary.Count + 1, // Use MainWindow's shared ReportsDictionary
                Location = LocationTextBox.Text,
                Category = ((KeyValuePair<int, string>)CategoryComboBox.SelectedItem).Value,
                Description = new TextRange(DescriptionRichTextBox.Document.ContentStart, DescriptionRichTextBox.Document.ContentEnd).Text.Trim(),
                MediaPath = attachedMediaPath
            };

            _mainWindow.ReportsDictionary.Add(newReport.Id, newReport); // Add to MainWindow's dictionary
            MessageBox.Show("Report submitted successfully!");
            ClearForm();
        }

        private void ClearForm()
        {
            LocationTextBox.Clear();
            CategoryComboBox.SelectedIndex = -1;
            DescriptionRichTextBox.Document.Blocks.Clear();
            attachedMediaPath = null;
        }
        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png|All Files|*.*"; // Allow image files
            bool? response = openFileDialog.ShowDialog();

            if (response == true)
            {
                attachedMediaPath = openFileDialog.FileName;
                MessageBox.Show($"Media attached: {attachedMediaPath}"); // Notify the user
            }
            else
            {
                MessageBox.Show("No file selected.");
            }
        }


        // Event handler for View Reports button
        private void ViewReportsButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.MainContent.Content = new ViewReports(_mainWindow.ReportsDictionary); // Pass MainWindow's reportsDictionary
        }
    }
}
