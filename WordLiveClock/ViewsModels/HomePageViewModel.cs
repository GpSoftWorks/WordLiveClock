using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Timers;

namespace WordLiveClock.ViewModels
{
    public partial class HomePageViewModel : ObservableObject
    {
        public ObservableCollection<CityClockModel> CityClocks { get; set; } = new();

        private readonly System.Timers.Timer _timer;

        public HomePageViewModel()
        {
            // Country-to-time zone mapping (Expanded for multiple countries)
            var countryToTimeZone = new Dictionary<string, string>
            {
                { "United States", "Eastern Standard Time" },
                { "United Kingdom", "GMT Standard Time" },
                { "Japan", "Tokyo Standard Time" },
                { "India", "India Standard Time" },
                { "Germany", "Central European Standard Time" },
                { "Australia", "AUS Eastern Standard Time" },
                { "Brazil", "E. South America Standard Time" },
                { "Russia", "Russian Standard Time" },
                { "China", "China Standard Time" },
                { "South Africa", "South Africa Standard Time" },
                { "Argentina", "Argentina Standard Time" },
                { "Belgium", "Central European Standard Time" },
                { "Canada", "Pacific Standard Time" },
                { "Mexico", "Central Standard Time" },
                { "France", "Central European Standard Time" },
                { "Italy", "Central European Standard Time" },
                { "Spain", "Central European Standard Time" },
                { "Netherlands", "Central European Standard Time" },
                { "Sweden", "Central European Standard Time" },
                { "Switzerland", "Central European Standard Time" },
                { "New Zealand", "New Zealand Standard Time" },
                { "South Korea", "Korea Standard Time" },
                { "Turkey", "Turkey Standard Time" },
                { "Egypt", "Eastern European Standard Time" },
                { "Poland", "Central European Standard Time" },
                { "Saudi Arabia", "Arabian Standard Time" },
                { "Singapore", "Singapore Standard Time" },
                { "Thailand", "Indochina Time" },
                { "Indonesia", "Western Indonesia Time" },
                { "Nigeria", "West Africa Time" },
                { "Ukraine", "Eastern European Standard Time" },
                { "Vietnam", "Indochina Time" },
                // Add more countries here...
            };

            // Populate the CityClocks collection
            foreach (var kvp in countryToTimeZone)
            {
                CityClocks.Add(new CityClockModel
                {
                    CountryName = kvp.Key,
                    TimeZoneId = kvp.Value,
                    Date = GetCityTime(kvp.Value).ToString("yyyy-MM-dd"),
                    Time = GetCityTime(kvp.Value).ToString("hh:mm:ss tt") // AM/PM format
                });
            }

            // Set up a timer to update times every second
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += UpdateTimes;
            _timer.Start();
        }

        private void UpdateTimes(object? sender, ElapsedEventArgs e)
        {
            foreach (var clock in CityClocks)
            {
                var currentTime = GetCityTime(clock.TimeZoneId);
                clock.Date = currentTime.ToString("yyyy-MM-dd");
                clock.Time = currentTime.ToString("hh:mm:ss tt"); // Update time in AM/PM format
            }
        }


        private static DateTime GetCityTime(string? timeZoneId)
        {
            if (string.IsNullOrEmpty(timeZoneId))
            {
                return DateTime.UtcNow; // Fallback to UTC if timeZoneId is null or empty
            }

            try
            {
                return TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
            }
            catch
            {
                return DateTime.UtcNow; // Fallback to UTC if the time zone is invalid
            }
        }

    }

    public partial class CityClockModel : ObservableObject
    {
        private string? countryName;
        private string? timeZoneId;
        private string? date;
        private string? time;

        public string? CountryName
        {
            get => countryName;
            set => SetProperty(ref countryName, value);
        }

        public string? TimeZoneId
        {
            get => timeZoneId;
            set => SetProperty(ref timeZoneId, value);
        }

        public string? Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public string? Time
        {
            get => time;
            set => SetProperty(ref time, value);
        }
    }
}
