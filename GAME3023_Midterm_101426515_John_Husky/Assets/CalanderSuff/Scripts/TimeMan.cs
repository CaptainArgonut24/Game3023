using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        [SerializeField] private TextMeshProUGUI dateText;
        [SerializeField] private TextMeshProUGUI monthText;
        [SerializeField] private TextMeshProUGUI yearText;
        [SerializeField] private TextMeshProUGUI weekText;
        [SerializeField] private List<TextMeshProUGUI> dayOfWeekTexts;

        [Header("Calendar Grid")]
        [SerializeField] private List<GameObject> calendarDays;

        [Header("Tick Settings")]
        [SerializeField] private int TickIncreaseMinutes = 1;
        [SerializeField] private float TimeBetweenTicks = 0.5f;
        private float currentTickTime = 0f;

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
        }

        private void AdvanceTime()
        {
            // Add minutes and handle overflow
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

            UpdateUI();
        }

        private void AdvanceDay()
        {
            // Add day and handle month overflow
            day++;

            // Check for leap year adjustment in February
            int maxDaysInMonth = (month == 2 && IsLeapYear(year)) ? 29 : daysInMonths[month - 1];

            if (day > maxDaysInMonth)
            {
                day = 1;
                month++;

                if (month > 12)
                {
                    month = 1;
                    year++;
                }
            }
        }

        private bool IsLeapYear(int year)
        {
            return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        }

        private void UpdateUI()
        {
            // Update Date in MM/DD/YYYY format
            if (dateText != null)
            {
                dateText.text = $"{month:D2}/{day:D2}/{year}";
            }

            // Update Month Name
            if (monthText != null)
            {
                monthText.text = monthNames[month - 1];
            }

            // Update Year
            if (yearText != null)
            {
                yearText.text = $"Year: {year}";
            }

            // Update Week and Day
            int dayOfWeekIndex = (day - 1) % 7;
            if (weekText != null)
            {
                weekText.text = $"Day: {dayNames[dayOfWeekIndex]}";
            }

            // Update day-of-week text colors
            for (int i = 0; i < dayOfWeekTexts.Count; i++)
            {
                if (dayOfWeekTexts[i] != null)
                {
                    dayOfWeekTexts[i].color = (i == dayOfWeekIndex) ? Color.green : Color.white;
                }
            }

            // Update Calendar Grid
            for (int i = 0; i < calendarDays.Count; i++)
            {
                var renderer = calendarDays[i].GetComponent<SpriteRenderer>();
                if (renderer != null)
                {
                    renderer.color = (i == day - 1) ? Color.green : Color.white;
                }
            }
        }

        #region Public Methods
        public void LinkCalendarDays(List<GameObject> days)
        {
            calendarDays = days;
        }

        public void LinkDayOfWeekTexts(List<TextMeshProUGUI> dayTexts)
        {
            dayOfWeekTexts = dayTexts;
        }
        #endregion
    }
}
