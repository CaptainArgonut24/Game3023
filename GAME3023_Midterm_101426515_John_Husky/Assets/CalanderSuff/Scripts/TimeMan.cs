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
        [SerializeField] private int TickIncrease = 1;
        [SerializeField] private float TimeBetweenTicks = 0.5f;
        private float currentTickTime = 0f;

        private string[] dayNames = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

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
            minute += TickIncrease;

            if (minute >= 60)
            {
                minute = 0;
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
            day++;

            if (day > 28)
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

        private void UpdateUI()
        {
            // Update Date, Month, and Year
            dateText.text = $"Date: {day:D2}";
            monthText.text = $"Month: {month:D2}";
            yearText.text = $"Year: {year}";

            // Update Week and Day
            int dayOfWeekIndex = (day - 1) % 7;
            weekText.text = $"Day: {dayNames[dayOfWeekIndex]}";

            // Update day-of-week text colors
            for (int i = 0; i < dayOfWeekTexts.Count; i++)
            {
                dayOfWeekTexts[i].color = (i == dayOfWeekIndex) ? Color.green : Color.white;
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
