using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calendar : MonoBehaviour
{     
    public static Calendar Instance;
    public DayDataUI DayDataUI;
    public GameObject CalendarCanvas;
    public GameObject DayCanvas;
    public MonthCreator MonthCreator;
    int currentYear;
    int currentMonth;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        Instance = this;
    }

    public void Start()
    {
        currentYear = DateTime.Now.Year;
        currentMonth = DateTime.Now.Month;
        CalendarDay.OnCalendarDayClick += (x) => ToggleFullCalendarWithDay();
        CalendarDay.OnCalendarDayClick += (x) => SetDataForDayDataUI(x);
        DayDataUI.OnSave += (x) => ToggleFullCalendarWithDay();
        UpdateCal();
    }
    void SetDataForDayDataUI(DayData dayData)
    {
        DayDataUI.SetData(dayData);
    }
    void ToggleFullCalendarWithDay()
    {
        CalendarCanvas.SetActive(!CalendarCanvas.activeInHierarchy);
        DayCanvas.SetActive(!CalendarCanvas.activeInHierarchy);
    }
    void UpdateCal()
    {
        MonthCreator.UpdateMonth(currentYear, currentMonth);
    }

    public void NextMonth()
    {
        if (currentMonth == 12)
        {
            currentYear++;
            currentMonth = 1;
        }
        else
            currentMonth++;
        UpdateCal();
    }

    public void PrevMonth()
    {
        if (currentMonth == 1)
        {
            currentYear--;
            currentMonth = 12;
        }
        else
            currentMonth--;
        UpdateCal();
    }

    public void SetMonth(int month)
    {
        currentMonth = month;
        UpdateCal();
    }

    public void SetYear(int year)
    {
        currentYear = year;
    }
}
