using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Timers;

namespace WordLiveClock.ViewModels
{
    public partial class HomePageViewModel : ObservableObject
    {
        public string VersionNumber { get; set; }

        public ObservableCollection<CityClockModel> CityClocks { get; set; } = new();

        private readonly System.Timers.Timer _timer;

        public HomePageViewModel()
        {
            VersionNumber = VersionTracking.CurrentVersion;
            // Country-to-time zone and language mapping
            var countryData = new Dictionary<string, (string TimeZone, string Language)>
            {
                { "United States", ("Eastern Standard Time", "English") },
                { "United Kingdom", ("GMT Standard Time", "English") },
                { "Japan", ("Tokyo Standard Time", "Japanese") },
                { "India", ("India Standard Time", "Hindi") },
                { "Germany", ("Central European Standard Time", "German") },
                { "Australia", ("AUS Eastern Standard Time", "English") },
                { "Brazil", ("E. South America Standard Time", "Portuguese") },
                { "Russia", ("Russian Standard Time", "Russian") },
                { "China", ("China Standard Time", "Mandarin") },
                { "South Africa", ("South Africa Standard Time", "Zulu") },
                { "Argentina", ("Argentina Standard Time", "Spanish") },
                { "Belgium", ("Central European Standard Time", "Dutch, French, German") },
                { "Canada", ("Pacific Standard Time", "English, French") },
                { "Mexico", ("Central Standard Time", "Spanish") },
                { "France", ("Central European Standard Time", "French") },
                { "Italy", ("Central European Standard Time", "Italian") },
                { "Spain", ("Central European Standard Time", "Spanish") },
                { "Netherlands", ("Central European Standard Time", "Dutch") },
                { "Sweden", ("Central European Standard Time", "Swedish") },
                { "Switzerland", ("Central European Standard Time", "German, French, Italian") },
                { "New Zealand", ("New Zealand Standard Time", "English, Māori") },
                { "South Korea", ("Korea Standard Time", "Korean") },
                { "Turkey", ("Turkey Standard Time", "Turkish") },
                { "Egypt", ("Eastern European Standard Time", "Arabic") },
                { "Poland", ("Central European Standard Time", "Polish") },
                { "Saudi Arabia", ("Arabian Standard Time", "Arabic") },
                { "Singapore", ("Singapore Standard Time", "English, Malay, Mandarin, Tamil") },
                { "Thailand", ("Indochina Time", "Thai") },
                { "Indonesia", ("Western Indonesia Time", "Indonesian") },
                { "Nigeria", ("West Africa Time", "English") },
                { "Ukraine", ("Eastern European Standard Time", "Ukrainian") },
                { "Vietnam", ("Indochina Time", "Vietnamese") },
                // Add more countries here...
            };

            // Populate the CityClocks collection
            foreach (var kvp in countryData)
            {
                CityClocks.Add(new CityClockModel
                {
                    CountryName = kvp.Key,
                    TimeZoneId = kvp.Value.TimeZone,
                    CountryLanguage = kvp.Value.Language,
                    Date = GetCityTime(kvp.Value.TimeZone).ToString("yyyy-MM-dd"),
                    Time = GetCityTime(kvp.Value.TimeZone).ToString("hh:mm:ss tt") // AM/PM format
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
        private string? countryLanguage;

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

        public string? CountryLanguage
        {
            get => countryLanguage;
            set => SetProperty(ref countryLanguage, value);
        }
    }
}
