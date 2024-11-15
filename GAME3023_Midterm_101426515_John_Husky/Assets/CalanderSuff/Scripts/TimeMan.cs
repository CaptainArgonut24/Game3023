using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace Calendar
{
    public class TimeMan : MonoBehaviour
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
        public DateFormat dateFormat = DateFormat.MM_DD_YY; // Dropdown for date format selection

        [Header("Text Prefabs")]
        [SerializeField] private GameObject dateTextPrefab;
        [SerializeField] private GameObject timeTextPrefab;
        [SerializeField] private GameObject yearTextPrefab;
        [SerializeField] private GameObject weekTextPrefab;
        [SerializeField] private GameObject monthTextPrefab;

        private DateTime DateTime;
        public static UnityAction<DateTime> OnDateTimeChanged;

        [Header("Personal Tick Settings")]
        [SerializeField]
        public int TickIncrease = 1;
        [SerializeField]
        public float TimeBetweenTicks = 0.5f;
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
            UpdateUI();
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
            UpdateUI();
        }

        private void UpdateUI()
        {
            // Update Date
            if (dateTextPrefab != null)
            {
                TextMeshPro dateTextMesh = dateTextPrefab.GetComponent<TextMeshPro>();
                if (dateTextMesh != null)
                {
                    dateTextMesh.text = DateTime.DateString(dateFormat);
                }
                else
                {
                    Debug.LogError("TextMeshPro component is missing from the Date prefab.");
                }
            }

            // Update Time
            if (timeTextPrefab != null)
            {
                TextMeshPro timeTextMesh = timeTextPrefab.GetComponent<TextMeshPro>();
                if (timeTextMesh != null)
                {
                    timeTextMesh.text = DateTime.TimeString(is24HourTime);
                }
                else
                {
                    Debug.LogError("TextMeshPro component is missing from the Time prefab.");
                }
            }

            // Update Year
            if (yearTextPrefab != null)
            {
                TextMeshPro yearTextMesh = yearTextPrefab.GetComponent<TextMeshPro>();
                if (yearTextMesh != null)
                {
                    yearTextMesh.text = DateTime.YearString();
                }
                else
                {
                    Debug.LogError("TextMeshPro component is missing from the Year prefab.");
                }
            }

            // Update Week
            if (weekTextPrefab != null)
            {
                TextMeshPro weekTextMesh = weekTextPrefab.GetComponent<TextMeshPro>();
                if (weekTextMesh != null)
                {
                    weekTextMesh.text = DateTime.WeekString();
                }
                else
                {
                    Debug.LogError("TextMeshPro component is missing from the Week prefab.");
                }
            }

            // Update Month
            if (monthTextPrefab != null)
            {
                TextMeshPro monthTextMesh = monthTextPrefab.GetComponent<TextMeshPro>();
                if (monthTextMesh != null)
                {
                    monthTextMesh.text = DateTime.Season.ToString();
                }
                else
                {
                    Debug.LogError("TextMeshPro component is missing from the Month prefab.");
                }
            }
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
                MonthMode.FourMonth => 31,
                MonthMode.SixMonth => 31,
                MonthMode.TwelveMonth => 30,
                _ => 30,
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
        public string DateString(DateFormat format)
        {
            return format switch
            {
                DateFormat.MM_DD_YY => $"{(int)season:D2}/{date:D2}/{year % 100:D2}",
                DateFormat.DD_MM_YYYY => $"{date:D2}/{(int)season:D2}/{year}",
                DateFormat.YYYY_MM_DD => $"{year:D4}-{(int)season:D2}-{date:D2}",
                _ => $"{(int)season:D2}/{date:D2}/{year % 100:D2}"
            };
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
    }

    #region Enums
    [Serializable]
    public enum Days { NULL = 0, Monday = 1, Tuesday = 2, Wednesday = 3, Thursday = 4, Friday = 5, Saturday = 6, Sunday = 7 }
    [Serializable]
    public enum Season { Spring = 1, Summer = 2, Fall = 3, Winter = 4 }
    public enum MonthMode { FourMonth = 4, SixMonth = 6, TwelveMonth = 12 }
    public enum DateFormat { MM_DD_YY, DD_MM_YYYY, YYYY_MM_DD }
    #endregion
}
