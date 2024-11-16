using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Mathematics;

namespace Calendar
{
    public class TimeMan : MonoBehaviour
    {
        #region Variables
        [Header("Date and Time Settings")]
        public int hour = 0;
        public int minute = 0;
        public int day = 1;
        public int month = 1;
        public int year = 2024;

        public bool is24HourTime = true;

        [Header("Text Prefabs")]
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private TextMeshProUGUI dateText;
        [SerializeField] private TextMeshProUGUI monthText;
        [SerializeField] private TextMeshProUGUI yearText;
        [SerializeField] private TextMeshProUGUI weekText;
        [SerializeField] private TextMeshProUGUI dayText;

        [SerializeField] private List<TextMeshProUGUI> dayOfWeekTexts;

        [Header("Calendar Grid")]
        [SerializeField] private List<GameObject> calendarDays;  // These are the day blocks (prefabs)

        [Header("Tick Settings")]
        [SerializeField] private int TickIncreaseMinutes = 1;
        [SerializeField] private float TimeBetweenTicks = 0.5f;
        private float currentTickTime = 0f;

        [Header("Weather System")]
        [SerializeField] private List<Sprite> dayWeatherSprites; // 10 for day weather
        [SerializeField] private List<Sprite> nightWeatherSprites; // 10 for night weather
        [SerializeField] private GameObject[] weatherObjects; // Array to hold weather  GameObjects
        [SerializeField] private GameObject[] temperatureTextObjects; // Array to hold temperature text GameObjects
        [SerializeField] private GameObject[] weatherTypeTextObjects; // Array to hold weather type text GameObjects
        [SerializeField] private GameObject[] seasonTextObjects; // Array to hold season text GameObjects
        [SerializeField] private GameObject[] forecastTextObjects; // Array to hold forecast text GameObjects



        [SerializeField] private GameObject weatherDisplay; // To display weather-related data (e.g., temperature, weather type)



        [System.Serializable]
        public class CalendarEvent
        {
            public string eventName;
            public string eventDescription;
            public int eventDay;  // The day the event occurs (1-28)
            public int eventMonth; // The month of the event (1-12)
            public int eventYear;  // The year of the event (e.g., 2024)
        }
        

        // Assign events to the calendar based on the current date
        

       

        private GameObject CreateEventObject(CalendarEvent calendarEvent)
        {
            // This is just an example; you can customize how you want to represent the event in your UI
            GameObject eventObject = new GameObject(calendarEvent.eventName);
            // Add any relevant components like TextMeshPro or UI elements to display the event's details
            eventObject.AddComponent<TextMeshProUGUI>().text = calendarEvent.eventName + ": " + calendarEvent.eventDescription;
            return eventObject;
        }

       



        private string[] dayNames = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        private string[] monthNames =
        {
            "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        };

        private int[] daysInMonths =
        {
            31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31
        };

        public enum DateFormat
        {
            YYYY_MM_DD,
            MM_DD_YY,
            DD_MM_YYYY
        }

        [Header("Date Format Settings")]
        public DateFormat currentDateFormat = DateFormat.YYYY_MM_DD;

        #endregion

        private void Start()
        {
            UpdateUI();
        }


        private void Update()
        {
            currentTickTime += Time.deltaTime;

            if (currentTickTime >= TimeBetweenTicks)
            {
                currentTickTime = 0;
                AdvanceTime();
            }

            // Ensure UI is updated every frame to reflect time changes
            UpdateUI();
        }

        private void AdvanceTime()
        {
            minute += TickIncreaseMinutes;

            if (minute >= 60)
            {
                minute -= 60;
                hour++;

                if (hour >= 24)
                {
                    hour = 0;
                    AdvanceDay();
                }
            }
        }

        private void AdvanceDay()
        {
            day++;

            int maxDaysInMonth = (month == 2 && IsLeapYear(year)) ? 29 : daysInMonths[month - 1];

            // Check if the current day exceeds the max days for the current month (28 days for now)
            if (day > 28)  // Reset to the first day after 28
            {
                day = 1;
                month++;

                // If the month exceeds 12, reset to January and increment the year
                if (month > 12)
                {
                    month = 1;
                    year++;
                }
            }

            UpdateCalendarDays();
        }


        private bool IsLeapYear(int year)
        {
            return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        }

        private void UpdateUI()
        {
            if (timeText != null)
            {
                string timeFormat = is24HourTime ? $"{hour:D2}:{minute:D2}" : FormatTimeTo12Hour(hour, minute);
                timeText.text = timeFormat;
            }

            if (dateText != null)
            {
                dateText.text = GetFormattedDate();
            }

            if (monthText != null)
            {
                monthText.text = monthNames[month - 1];
            }

            if (yearText != null)
            {
                yearText.text = $"Year: {year}";
            }

            int weekNumber = GetWeekOfYear(year, month, day);
            if (weekText != null)
            {
                weekText.text = $"Week: {weekNumber}";
            }

            if (dayText != null)
            {
                int dayOfWeekIndex = GetDayOfWeekIndex(year, month, day);
                dayText.text = dayNames[dayOfWeekIndex]; // Update to show the full day name
                UpdateDayOfWeekHighlight(dayOfWeekIndex);
            }

            UpdateCalendarDays();
        }

        private string FormatTimeTo12Hour(int hour, int minute)
        {
            string period = hour >= 12 ? "PM" : "AM";
            int hour12 = hour % 12;
            if (hour12 == 0) hour12 = 12; // Handle midnight/noon
            return $"{hour12:D2}:{minute:D2} {period}";
        }

        private int GetDayOfWeekIndex(int year, int month, int day)
        {
            DateTime date = new DateTime(year, month, day);
            return (int)date.DayOfWeek;
        }

        private void UpdateDayOfWeekHighlight(int currentDayOfWeek)
        {
            // Reset the color of all day texts
            for (int i = 0; i < dayOfWeekTexts.Count; i++)
            {
                dayOfWeekTexts[i].color = Color.white;  // Reset all day text colors to white
            }

            // Determine the corresponding days to highlight
            int[] daysForHighlight = { 1, 8, 15, 22 };  // Sunday
            if (Array.Exists(daysForHighlight, d => d == day))
            {
                dayOfWeekTexts[0].color = Color.green;  // Sunday
                dayText.text = "Sunday";  // Set day text to Sunday
            }
            daysForHighlight = new int[] { 2, 9, 16, 23 };  // Monday
            if (Array.Exists(daysForHighlight, d => d == day))
            {
                dayOfWeekTexts[1].color = Color.green;  // Monday
                dayText.text = "Monday";  // Set day text to Monday
            }
            daysForHighlight = new int[] { 3, 10, 17, 24 };  // Tuesday
            if (Array.Exists(daysForHighlight, d => d == day))
            {
                dayOfWeekTexts[2].color = Color.green;  // Tuesday
                dayText.text = "Tuesday";  // Set day text to Tuesday
            }
            daysForHighlight = new int[] { 4, 11, 18, 25 };  // Wednesday
            if (Array.Exists(daysForHighlight, d => d == day))
            {
                dayOfWeekTexts[3].color = Color.green;  // Wednesday
                dayText.text = "Wednesday";  // Set day text to Wednesday
            }
            daysForHighlight = new int[] { 5, 12, 19, 26 };  // Thursday
            if (Array.Exists(daysForHighlight, d => d == day))
            {
                dayOfWeekTexts[4].color = Color.green;  // Thursday
                dayText.text = "Thursday";  // Set day text to Thursday
            }
            daysForHighlight = new int[] { 6, 13, 20, 27 };  // Friday
            if (Array.Exists(daysForHighlight, d => d == day))
            {
                dayOfWeekTexts[5].color = Color.green;  // Friday
                dayText.text = "Friday";  // Set day text to Friday
            }
            daysForHighlight = new int[] { 7, 14, 21, 28 };  // Saturday
            if (Array.Exists(daysForHighlight, d => d == day))
            {
                dayOfWeekTexts[6].color = Color.green;  // Saturday
                dayText.text = "Saturday";  // Set day text to Saturday
            }
        }

        private void UpdateCalendarDays()
        {
            // Loop through each day block in the calendar grid
            for (int i = 0; i < calendarDays.Count; i++)
            {
                var dayBlock = calendarDays[i];
                var dayImage = dayBlock.GetComponent<Image>();

                if (dayImage != null)
                {
                    int currentDay = i + 1;  // Index from 0 to 27, corresponding to days 1 to 28 in the calendar
                    SetDayColor(dayImage, currentDay);
                }
            }
        }

        private void SetDayColor(Image dayImage, int currentDay)
        {
            if (currentDay == day)
            {
                dayImage.color = Color.green;   // Current day is green
            }
            else if (currentDay < day)
            {
                dayImage.color = Color.red;      // Past days are red
            }
            else
            {
                dayImage.color = Color.white;    // Future days are white
            }
        }

        private string GetFormattedDate()
        {
            switch (currentDateFormat)
            {
                case DateFormat.YYYY_MM_DD:
                    return $"{year}-{month:D2}-{day:D2}";
                case DateFormat.MM_DD_YY:
                    return $"{month:D2}/{day:D2}/{year % 100:D2}";
                case DateFormat.DD_MM_YYYY:
                    return $"{day:D2}-{month:D2}-{year}";
                default:
                    return string.Empty;
            }
        }

        private int GetWeekOfYear(int year, int month, int day)
        {
            DateTime date = new DateTime(year, month, day);
            System.Globalization.Calendar cal = System.Globalization.CultureInfo.InvariantCulture.Calendar;
            return cal.GetWeekOfYear(date, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }
    }

    public class WeatherManager : MonoBehaviour
    {
        [Header("Weather System")]
        [SerializeField] private List<Sprite> dayWeatherSprites; // 10 for day weather
        [SerializeField] private List<Sprite> nightWeatherSprites; // 10 for night weather
        [SerializeField]
        private List<string> weatherTypes = new List<string>
        {
            "Clear", "Partly Cloudy", "Rain", "Thunder", "Sleet", "Snow", "ThunderSnow",
            "Cloudy", "Fog", "Tornado"
        };
        [SerializeField] private GameObject[] weatherObjects; // Array to hold weather GameObjects
        [SerializeField] private GameObject[] temperatureTextObjects; // Array to hold temperature text GameObjects
        [SerializeField] private GameObject[] weatherTypeTextObjects; // Array to hold weather type text GameObjects
        [SerializeField] private GameObject[] seasonTextObjects; // Array to hold season text GameObjects
        [SerializeField] private GameObject[] forecastTextObjects; // Array to hold forecast text GameObjects

        private string currentWeather;
        private string currentSeason;
        private List<string> dailyForecast = new List<string>();

        private TimeMan timeMan; // Reference to TimeMan script

        void Start()
        {
            timeMan = GetComponent<TimeMan>();
            UpdateWeather();
        }

        void Update()
        {
            UpdateWeather();
        }

        private void UpdateWeather()
        {
            // Determine current weather based on time and season
            currentWeather = GetWeatherForCurrentDay();

            // Get the current season
            currentSeason = GetSeasonForCurrentMonth();

            // Update weather and season displays
            UpdateWeatherUI();
            UpdateSeasonUI();
            UpdateForecastUI();
        }

        private string GetWeatherForCurrentDay()
        {
            // Randomly select a weather type from the list
            return weatherTypes[UnityEngine.Random.Range(0, weatherTypes.Count)];
        }

        private string GetSeasonForCurrentMonth()
        {
            // Determine season based on the current month
            if (timeMan.month >= 3 && timeMan.month <= 5)
                return "Spring";
            else if (timeMan.month >= 6 && timeMan.month <= 8)
                return "Summer";
            else if (timeMan.month >= 9 && timeMan.month <= 11)
                return "Fall";
            else
                return "Winter";
        }

        private void UpdateWeatherUI()
        {
            // Update weather objects and text
            int weatherIndex = UnityEngine.Random.Range(0, dayWeatherSprites.Count);
            foreach (var obj in weatherObjects)
            {
                obj.GetComponent<Image>().sprite = dayWeatherSprites[weatherIndex];
            }

            foreach (var textObj in weatherTypeTextObjects)
            {
                textObj.GetComponent<TextMeshProUGUI>().text = currentWeather;
            }

            foreach (var tempObj in temperatureTextObjects)
            {
                tempObj.GetComponent<TextMeshProUGUI>().text = "Temperature: " + GetTemperature() + "°C";
            }
        }

        private void UpdateSeasonUI()
        {
            // Update season text
            foreach (var seasonText in seasonTextObjects)
            {
                seasonText.GetComponent<TextMeshProUGUI>().text = "Season: " + currentSeason;
            }
        }

        private void UpdateForecastUI()
        {
            // Update daily forecast text
            dailyForecast.Clear();
            for (int i = 0; i < 7; i++)  // 7-day forecast
            {
                dailyForecast.Add(weatherTypes[UnityEngine.Random.Range(0, weatherTypes.Count)]);
            }

            for (int i = 0; i < forecastTextObjects.Length; i++)
            {
                if (i < dailyForecast.Count)
                {
                    forecastTextObjects[i].GetComponent<TextMeshProUGUI>().text = "Day " + (i + 1) + ": " + dailyForecast[i];
                }
            }
        }

        private string GetTemperature()
        {
            // Simple temperature calculation based on season
            if (currentSeason == "Winter")
                return UnityEngine.Random.Range(-10, 5).ToString();
            else if (currentSeason == "Spring")
                return UnityEngine.Random.Range(5, 15).ToString();
            else if (currentSeason == "Summer")
                return UnityEngine.Random.Range(20, 35).ToString();
            else // Fall
                return UnityEngine.Random.Range(10, 20).ToString();
        }
    }
}




