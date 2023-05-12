using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonthCreator : MonoBehaviour
{
    [SerializeField] GameObject WeekTemplate;
    [SerializeField] CalendarDay CalendarDayTemplate;
    [SerializeField] TextMeshProUGUI YearText;
    [SerializeField] TextMeshProUGUI MonthText;

    List<DayData> DaysDats = new List<DayData>();
    List<CalendarDay> CalendarDays = new List<CalendarDay>();

    Dictionary<int, string> MonthsNames = new Dictionary<int, string>
    {
        {1 , "Jan" },
        {2 , "Feb" },
        {3 , "Mar" },
        {4 , "Apr" },
        {5 , "May" },
        {6 , "Jun" },
        {7 , "Jul" },
        {8 , "Aug" },
        {9 , "Sep" },
        {10 , "Oct" },
        {11 , "Nov" },
        {12 , "Dec" }
    };


    public void UpdateMonth(int year, int month)
    {
        DestroyAll();
        YearText.text = year.ToString();
        MonthText.text = MonthsNames[month];
        DaysDats = new List<DayData>();
        CalendarDays = new List<CalendarDay>();
        int day = 1;
        for (int w = 0; w < 6; w++)
        {
            GameObject currentWeek = Instantiate(WeekTemplate, transform);
            for (int d = 0; d < 7; d++)
            {
                DayData dayData = new DayData();
                CalendarDay calendarDay = Instantiate(CalendarDayTemplate, currentWeek.transform);
                int currDay = w * 7 + d;
                bool isToday = CheckIsToday(year, month, currDay);
                int startDay = GetStartingDay(year, month);
                if (currDay < startDay)
                    UpdateDay(dayData, calendarDay, 0);
                else if (currDay > DateTime.DaysInMonth(year, month) + startDay - 1)
                    UpdateDay(dayData, calendarDay, 0);
                else
                    UpdateDay(dayData, calendarDay, day++, isToday);
            }
        }
    }

    bool CheckIsToday(int year, int month, int currDay)
    {
        if (DateTime.Now.Year != year)
            return false;
        if (DateTime.Now.Month != month)
            return false;
        if (DateTime.Today.Day != currDay)
            return false;
        return true;
    }

    private void DestroyAll()
    {
        Week[] weeks = gameObject.GetComponentsInChildren<Week>();
        foreach (Week currentWeek in weeks)
        {
            Destroy(currentWeek.gameObject);
        }
        DaysDats.Clear();
    }

    int GetStartingDay(int year, int month)
    {
        DateTime temp = new DateTime(year, month, 1);
        return (int)temp.DayOfWeek;
    }
    void UpdateDay(DayData currentDay, CalendarDay calendarDay, int dayNum, bool isToday = false)
    {
        currentDay.SetData(dayNum);
        calendarDay.InitDay(dayNum, currentDay, isToday);
    }
}
