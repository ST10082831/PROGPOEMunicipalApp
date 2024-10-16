using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using static POEPART1MunicipalApp.Models.EventClasses;

namespace POEPART1MunicipalApp.Views
{
    public partial class ViewEventDetails : UserControl
    {
        private Event _selectedEvent; // The event being displayed

        public ViewEventDetails(Event selectedEvent)
        {
            InitializeComponent();
            _selectedEvent = selectedEvent;
            DisplayEventDetails();
        }

      
        // Displays the details of the selected event.
        
        private void DisplayEventDetails()
        {
            TitleTextBlock.Text = _selectedEvent.Title;
            DateTextBlock.Text = _selectedEvent.Date.ToString("d MMM yyyy");
            LocationTextBlock.Text = _selectedEvent.Location;
            CategoryTextBlock.Text = _selectedEvent.Category;
            DescriptionTextBlock.Text = _selectedEvent.Description;

            // Load and display the event image
            if (!string.IsNullOrEmpty(_selectedEvent.ImagePath))
            {
                try
                {
                    string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _selectedEvent.ImagePath);

                    if (File.Exists(imagePath))
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(imagePath, UriKind.Absolute);
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();
                        EventImage.Source = bitmap;
                    }
                    else
                    {
                        EventImage.Source = null;
                        MessageBox.Show("Image file not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}");
                    EventImage.Source = null;
                }
            }
            else
            {
                EventImage.Source = null; // No image available
            }
        }

      
        // Handles the Back button click event to return to the Local Events page.
      
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.NavigateToPage(new LocalEvents());
        }
    }
}
