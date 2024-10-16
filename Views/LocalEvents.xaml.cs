using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static POEPART1MunicipalApp.Models.EventClasses;

namespace POEPART1MunicipalApp.Views
{
    public partial class LocalEvents : UserControl
    {
        private Dictionary<string, int> userCategoryPreferences = new Dictionary<string, int>(); // Dictionary to track user category preferences
        private MainWindow _mainWindow;           // Reference to the main window
        private List<Event> _displayedEvents;     // List of events currently displayed

        public LocalEvents()
        {
            InitializeComponent();
            _mainWindow = (MainWindow)Application.Current.MainWindow;
            PopulateCategoryComboBox();           // Populate the category filter
            LoadEvents();                         // Load events to display
        }


        /// Populates the category ComboBox with available event categories.

        private void PopulateCategoryComboBox()
        {
            var categories = _mainWindow.EventManager.Categories.ToList();
            categories.Insert(0, "All Categories"); // Add an option to show all categories
            CategoryComboBox.ItemsSource = categories;
            CategoryComboBox.SelectedIndex = 0;     // Set default selection
        }


        /// Loads events without any filters and displays them.

        private void LoadEvents()
        {
            try
            {
                // Get all events
                _displayedEvents = _mainWindow.EventManager.GetEvents(null, null, null);
                EventsListView.ItemsSource = _displayedEvents;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading events: {ex.Message}");
            }
        }


        /// Handles the Search button click event to filter events.

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectedCategory = CategoryComboBox.SelectedItem?.ToString();
                DateTime? selectedDate = DatePicker.SelectedDate;

                string categoryFilter = selectedCategory != "All Categories" ? selectedCategory : null;

                // Pass null for startDate and endDate if no date is selected
                DateTime? startDate = selectedDate?.Date;
                DateTime? endDate = selectedDate?.Date;

                // Get filtered events
                _displayedEvents = _mainWindow.EventManager.GetEvents(categoryFilter, startDate, endDate);
                EventsListView.ItemsSource = _displayedEvents;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering events: {ex.Message}");
            }
        }


        /// Handles the Clear button click event to reset filters.

        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryComboBox.SelectedIndex = 0;
            DatePicker.SelectedDate = null;
            LoadEvents();
        }


        /// Handles the Back button click event to return to the main menu.

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.ShowWelcomeMessage();  // Show welcome message on the main menu
            mainWindow.MainContent.Content = null; // Clear the content area
        }


        /// Handles the View Details button click event to display event details.

        private void ViewDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as Button;
                if (button != null)
                {
                    var eventId = (int)button.CommandParameter;
                    var selectedEvent = _displayedEvents.FirstOrDefault(evt => evt.Id == eventId);

                    if (selectedEvent != null)
                    {
                        var mainWindow = Window.GetWindow(this) as MainWindow;
                        mainWindow.NavigateToPage(new ViewEventDetails(selectedEvent));

                        // Track user preference
                        if (userCategoryPreferences.ContainsKey(selectedEvent.Category))
                        {
                            userCategoryPreferences[selectedEvent.Category]++;
                        }
                        else
                        {
                            userCategoryPreferences[selectedEvent.Category] = 1;
                        }

                        // Update recommendations
                        LoadRecommendations();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening event details: {ex.Message}");
            }
        }

        private void LoadRecommendations()
        {
            var preferredCategories = GetTopPreferredCategories(3); // Get top 3 preferred categories
            var recommendedEvents = _mainWindow.EventManager.GetRecommendedEvents(preferredCategories);

         
            RecommendationsListView.ItemsSource = recommendedEvents;
        }

        //Gets the top preferred categories based on user interactions.
     
        private List<string> GetTopPreferredCategories(int topN)
        {
            return userCategoryPreferences
                .OrderByDescending(kvp => kvp.Value)
                .Take(topN)
                .Select(kvp => kvp.Key)
                .ToList();
        }

    }
}

