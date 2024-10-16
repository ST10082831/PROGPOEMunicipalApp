using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using POEPART1MunicipalApp.Views;
using static POEPART1MunicipalApp.Models.ReportClasses;
using static POEPART1MunicipalApp.Models.EventClasses;

namespace POEPART1MunicipalApp.Views
{
    public partial class MainWindow : Window
    {
        public Dictionary<int, Report> ReportsDictionary { get; private set; } // Stores reports
        public EventManage EventManager { get; private set; }                 // Manages events

        public MainWindow()
        {
            InitializeComponent();
            ReportsDictionary = new Dictionary<int, Report>();
            EventManager = new EventManage();
        }

        // Handles the Report an Issue button click event.
       
        private void reportBtn_Click(object sender, RoutedEventArgs e)
        {
            var reportIssuesPage = new ReportIssues();
            NavigateToPage(reportIssuesPage);
        }

   
        // Handles the Local Events button click event.
    
        private void LocalEventsBtn_Click(object sender, RoutedEventArgs e)
        {
            var localEventsPage = new LocalEvents();
            NavigateToPage(localEventsPage);
        }

       
        /// Navigates to the specified page with animations.
       
        public void NavigateToPage(UserControl newPage)
        {
            // Hide welcome message
            WelcomePanel.Visibility = Visibility.Collapsed;

            // Create the fade-out animation for the old content
            DoubleAnimation fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(300));
            fadeOut.Completed += (s, e) =>
            {
                // Switch content after fade-out
                MainContent.Content = newPage;
                // Fade-in animation for the new content
                DoubleAnimation fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(300));
                newPage.BeginAnimation(UserControl.OpacityProperty, fadeIn);
            };

            // Begin fade-out animation
            if (MainContent.Content != null)
            {
                (MainContent.Content as UserControl).BeginAnimation(UserControl.OpacityProperty, fadeOut);
            }
            else
            {
                // No content to fade out, just set the new content
                MainContent.Content = newPage;
                newPage.Opacity = 0;
                DoubleAnimation fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(300));
                newPage.BeginAnimation(UserControl.OpacityProperty, fadeIn);
            }
        }

    
        // Shows the welcome message when returning to the main menu.
     
        public void ShowWelcomeMessage()
        {
            // Clear the content area
            MainContent.Content = null;
            // Display the welcome panel
            WelcomePanel.Visibility = Visibility.Visible;
        }

        private void ServiceRequestBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Service Requests feature is coming soon! Stay tuned for future updates.", "Coming Soon", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
