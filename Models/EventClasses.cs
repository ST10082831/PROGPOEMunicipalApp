using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;



///SOURCES https://learn.microsoft.com/en-us/dotnet/standard/collections/ ///

namespace POEPART1MunicipalApp.Models
{
    public class EventClasses
    {
        // Class representing an Event
        public class Event
        {
            public int Id { get; set; }                   // Unique identifier for the event
            public string Title { get; set; }             // Title of the event
            public string Category { get; set; }          // Category of the event
            public DateTime Date { get; set; }            // Date of the event
            public string Description { get; set; }       // Description of the event
            public string Location { get; set; }          // Location where the event takes place
            public string ImagePath { get; set; }

            //Source: https://stackoverflow.com/questions/11880946/how-to-load-image-to-wpf-in-runtime //
            public BitmapImage ImageSource
            {
                get
                {
                    if (!string.IsNullOrEmpty(ImagePath))
                    {
                        try
                        {
                            // Use GetResourceStream to load the image from the project's resources to display on events list
                            Uri resourceUri = new Uri($"pack://application:,,,/{ImagePath}", UriKind.Absolute);
                            BitmapImage bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.UriSource = resourceUri;
                            bitmap.CacheOption = BitmapCacheOption.OnLoad;
                            bitmap.EndInit();
                            return bitmap;
                        }
                        catch
                        {
                            // Handle any exceptions and return null if the image cannot be loaded
                            return null;
                        }
                    }
                    return null;
                }
            }// Path to the event image
        }

        // Class to manage events using advanced data structures
        public class EventManage
        {
            public List<Event> GetRecommendedEvents(List<string> preferredCategories)
            {
                List<Event> recommendedEvents = new List<Event>();

                foreach (var dateQueuePair in EventsByDate)
                {
                    foreach (var evt in dateQueuePair.Value)
                    {
                        if (preferredCategories.Contains(evt.Category))
                        {
                            recommendedEvents.Add(evt);
                        }
                    }
                }

                return recommendedEvents;

            }




            // SortedDictionary to store events sorted by date
            public SortedDictionary<DateTime, Queue<Event>> EventsByDate { get; private set; }
            // HashSet to store unique categories
            public HashSet<string> Categories { get; private set; }

            public EventManage()
            {
                EventsByDate = new SortedDictionary<DateTime, Queue<Event>>();
                Categories = new HashSet<string>();
                LoadSampleEvents(); // Load initial events
            }

            // Method to add an event to the manager
            public void AddEvent(Event newEvent)
            {
                // Add event to the sorted dictionary based on date
                if (!EventsByDate.ContainsKey(newEvent.Date.Date))
                {
                    EventsByDate[newEvent.Date.Date] = new Queue<Event>();
                }
                EventsByDate[newEvent.Date.Date].Enqueue(newEvent);

                // Add the event's category to the set of categories
                Categories.Add(newEvent.Category);
            }

            // Method to get events based on filters
            public List<Event> GetEvents(string categoryFilter, DateTime? startDate, DateTime? endDate)
            {
                List<Event> filteredEvents = new List<Event>();

                foreach (var dateQueuePair in EventsByDate)
                {
                    DateTime eventDate = dateQueuePair.Key;

                    // Check if the event date is within the specified range
                    if ((startDate == null || eventDate >= startDate.Value.Date) &&
                        (endDate == null || eventDate <= endDate.Value.Date))
                    {
                        foreach (var evt in dateQueuePair.Value)
                        {
                            // Check if the event matches the category filter
                            if (string.IsNullOrEmpty(categoryFilter) ||
                                evt.Category.Equals(categoryFilter, StringComparison.OrdinalIgnoreCase))
                            {
                                filteredEvents.Add(evt);
                            }
                        }
                    }
                }

                return filteredEvents;
            }

            // Load sample events into the manager
            private void LoadSampleEvents()
            {
                // Sample events with image paths
                var sampleEvents = new List<Event>
                {
                    new Event
                    {
                        Id = 1,
                        Title = "Cape Town Jazz Festival",
                        Category = "Music",
                        Date = DateTime.Now.AddDays(5),
                        Description = "Experience world-class jazz performances.",
                        Location = "Cape Town International Convention Centre",
                        ImagePath = "Images/JazzFestival.jpg"
                    },
                    new Event
                    {
                        Id = 2,
                        Title = "Two Oceans Marathon",
                        Category = "Sports",
                        Date = DateTime.Now.AddDays(10),
                        Description = "Join the world's most beautiful marathon.",
                        Location = "Cape Town",
                        ImagePath = "Images/Marathon.jpg"
                    },
                    new Event
                    {
                        Id = 3,
                        Title = "Kirstenbosch Summer Concerts",
                        Category = "Music",
                        Date = DateTime.Now.AddDays(15),
                        Description = "Enjoy live music in a botanical garden.",
                        Location = "Kirstenbosch National Botanical Garden",
                        ImagePath = "Images/KirstenboschConcert.jpg"
                    },
                    new Event
                    {
                        Id = 4,
                        Title = "Table Mountain Hike",
                        Category = "Outdoor",
                        Date = DateTime.Now.AddDays(2),
                        Description = "Guided hike up Table Mountain.",
                        Location = "Table Mountain",
                        ImagePath = "Images/TableMountain.jpg"
                    },
                    new Event
                    {
                        Id = 5,
                        Title = "Wine Tasting Tour",
                        Category = "Food & Drink",
                        Date = DateTime.Now.AddDays(7),
                        Description = "Explore the finest wines in the region.",
                        Location = "Stellenbosch Wine Routes",
                        ImagePath = "Images/WineTasting.jpg"
                    },
                    new Event
                    {
                        Id = 6,
                        Title = "Cape Town Cycle Tour",
                        Category = "Sports",
                        Date = DateTime.Now.AddDays(20),
                        Description = "Participate in the world's largest timed cycle race.",
                        Location = "Cape Town",
                        ImagePath = "Images/CycleTour.jpg"
                    },
                    new Event
                    {
                        Id = 7,
                        Title = "Cape Town International Film Market",
                        Category = "Film",
                        Date = DateTime.Now.AddDays(12),
                        Description = "Attend film screenings and industry workshops.",
                        Location = "V&A Waterfront",
                        ImagePath = "Images/FilmMarket.jpg"
                    },
                    new Event
                    {
                        Id = 8,
                        Title = "Art Exhibition at Zeitz MOCAA",
                        Category = "Art",
                        Date = DateTime.Now.AddDays(4),
                        Description = "Explore contemporary African art.",
                        Location = "Zeitz Museum of Contemporary Art Africa",
                        ImagePath = "Images/ZeitzMOCAA.jpg"
                    },
                    new Event
                    {
                        Id = 9,
                        Title = "Food Truck Friday",
                        Category = "Food & Drink",
                        Date = DateTime.Now.AddDays(1),
                        Description = "Enjoy delicious street food from local vendors.",
                        Location = "Century City",
                        ImagePath = "Images/FoodTruck.jpg"
                    },
                    new Event
                    {
                        Id = 10,
                        Title = "Robben Island Tour",
                        Category = "History",
                        Date = DateTime.Now.AddDays(3),
                        Description = "Visit the historic prison where Nelson Mandela was held.",
                        Location = "Robben Island",
                        ImagePath = "Images/RobbenIsland.jpg"
                    }
                };

                // Add each sample event to the manager
                foreach (var evt in sampleEvents)
                {
                    AddEvent(evt);
                }
            }
        }
    }
}



