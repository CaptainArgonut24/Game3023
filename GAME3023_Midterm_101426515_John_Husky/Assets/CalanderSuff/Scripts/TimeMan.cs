using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Calender
{
    public class TimeMan: MonoBehaviour
    {
        #region Variables
        [Header("Date and Time Settings")]
        public int hour;
        public int minute;
        public int season;
        public int date;
        public int year;

        public bool is24HourTime = true; // Toggle for 24-hour format
        public MonthMode monthMode = MonthMode.TwelveMonth; // Toggle for month mode

        private DateTime DateTime;

        public static UnityAction<DateTime> OnDateTimeChanged;

        [Header("Personal Tick Settings")]
        [SerializeField]
        public int TickIncrease = 1;
        [SerializeField]
        public float TimeBetweenTicks = .5f;
        [SerializeField]
        public float CurrentTickTime = 0;

        #endregion

        private void Awake()
        {
            DateTime = new DateTime(minute, hour, date, season - 1, year, monthMode);
        }

        void Start()
        {
            OnDateTimeChanged?.Invoke(DateTime);
        }

        void Update()
        {
            CurrentTickTime += Time.deltaTime;

            if (CurrentTickTime >= TimeBetweenTicks)
            {
                CurrentTickTime = 0;
                AdvanceTime();
            }
        }

        private void AdvanceTime()
        {
            DateTime.AdvanceMinutes(TickIncrease);
            OnDateTimeChanged?.Invoke(DateTime);
        }
    }

    [Serializable]
    public struct DateTime
    {
        #region Fields

        private int minutes;
        private int hour;
        private Days day;
        private int date;
        private int year;
        private Season season;
        private int totalNumDays;
        private int totalNumWeeks;
        private MonthMode monthMode;

        #endregion

        #region Properties

        public int Minutes => minutes;
        public int Hours => hour;
        public Days Days => day;
        public int Date => date;
        public int Year => year;
        public Season Season => season;
        public int TotalNumDays => totalNumDays;
        public int CurrentWeek => totalNumWeeks;

        #endregion

        #region Constructor
        public DateTime(int minutes, int hours, int date, int season, int year, MonthMode monthMode)
        {
            this.minutes = minutes;
            this.hour = hours;
            this.day = (Days)(date % 7 == 0 ? 7 : date % 7);
            this.date = date;
            this.season = (Season)season;
            this.year = year;
            this.monthMode = monthMode;

            totalNumDays = date + (28 * season);
            totalNumDays += year > 1 ? (112 * (year - 1)) : 0;
            totalNumWeeks = 1 + totalNumDays / 7;
        }
        #endregion

        #region Time Advancement

        public void AdvanceMinutes(int timeMovement)
        {
            minutes += timeMovement;

            if (minutes >= 60)
            {
                minutes %= 60;
                AdvanceHours();
            }
        }

        private void AdvanceHours()
        {
            hour++;
            if (hour == 24)
            {
                hour = 0;
                AdvanceDays();
            }
        }

        private void AdvanceDays()
        {
            totalNumDays++;
            day = day + 1 > Days.Sunday ? Days.Monday : day + 1;
            totalNumWeeks += day == Days.Monday ? 1 : 0;

            date++;
            if (date > GetDaysInMonth())
            {
                date = 1;
                AdvanceSeason();
            }
        }

        private int GetDaysInMonth()
        {
            return monthMode switch
            {
                MonthMode.OneMonth => 28,
                MonthMode.FourMonth => 28,
                MonthMode.SixMonth => 28,
                _ => 28, // Default to 12-month system
            };
        }

        private void AdvanceSeason()
        {
            season++;

            if (season > Season.Winter)
            {
                season = Season.Spring;
                AdvanceYear();
            }
        }

        private void AdvanceYear()
        {
            date = 1;
            year++;
        }

        #endregion

        #region Formatting Methods

        public string DateString()
        {
            return $"{day} / {Date} / {Year}";
        }

        public string TimeString(bool is24Hour)
        {
            int displayHour = is24Hour ? hour : (hour == 0 || hour == 12 ? 12 : hour % 12);
            string period = hour >= 12 ? "PM" : "AM";

            return is24Hour ? $"{hour:D2}:{minutes:D2}" : $"{displayHour}:{minutes:D2} {period}";
        }

        public string WeekString()
        {
            return $"Week: {CurrentWeek}";
        }

        public string YearString()
        {
            return $"Year: {year}";
        }

        #endregion

        public bool IsNight()
        {
            throw new NotImplementedException();
        }
    }

    #region Enums

    [Serializable]
    public enum Days
    {
        NULL = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7
    }

    [Serializable]
    public enum Season
    {
        Spring = 0,
        Summer = 1,
        Fall = 2,
        Winter = 3
    }

    public enum MonthMode
    {
        OneMonth = 1,
        FourMonth = 4,
        SixMonth = 6,
        TwelveMonth = 12
    }

    #endregion
}
