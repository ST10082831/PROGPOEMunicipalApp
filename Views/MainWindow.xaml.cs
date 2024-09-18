using System.Windows;
using static POEPART1MunicipleApp.Models.ReportClasses;

namespace POEPART1MunicipleApp.Views
{
    public partial class MainWindow : Window
    {
        public Dictionary<int, Report> ReportsDictionary { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            ReportsDictionary = new Dictionary<int, Report>(); // Store reports here
        }

        private void reportBtn_Click(object sender, RoutedEventArgs e)
        {
            // Load ReportIssues UserControl into the ContentControl
            MainContent.Content = new ReportIssues();
        }

        private void LocalEventsBtn_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void ServiceRequestBtn_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}