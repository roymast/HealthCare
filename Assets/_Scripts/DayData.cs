using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayData
{
    public int year;
    public int month;
    public int day;    
    public string BreakFast;
    public string Lanch;
    public string Dinner;
    public string Night;
    public List<string> Snacks;
    public int WaterInML;
    //public List<WorkOut> workouts;
    public int Tierness;
    public string GeneralFeel;
    public List<string> TodaysAchivments;
    public List<string> outs;

    public DayData(DayData dayData)
    {
        year = dayData.year;
        month = dayData.month;
        day = dayData.day;
        BreakFast = dayData.BreakFast;
        Lanch = dayData.Lanch;
        Dinner = dayData.Dinner;
        Night = dayData.Night;
        Snacks = dayData.Snacks;
        WaterInML = dayData.WaterInML;        
        Tierness = dayData.Tierness;
        GeneralFeel = dayData.GeneralFeel;
        TodaysAchivments = dayData.TodaysAchivments;
        outs = dayData.outs;
    }
    public DayData(int year, int month, int day)
    {
        this.year = year;
        this.month = month;
        this.day = day;
    }
    public void UpdateDayData(DayData dayData)
    {
        year = dayData.year;
        month = dayData.month;
        day = dayData.day;
        BreakFast = dayData.BreakFast;
        Lanch = dayData.Lanch;
        Dinner = dayData.Dinner;
        Night = dayData.Night;
        Snacks = dayData.Snacks;
        WaterInML = dayData.WaterInML;
        Tierness = dayData.Tierness;
        GeneralFeel = dayData.GeneralFeel;
        TodaysAchivments = dayData.TodaysAchivments;
        outs = dayData.outs;
    }
    public string ToSring()
    {
        return $"date: {day}/{month}/{year}\n" +
            $"BreakFast: {BreakFast}\n" +
            $"Lanch: {Lanch}\n" +
            $"Dinner: {Dinner}\n" +
            $"Night: {Night}\n" +
            $"WaterInML: {WaterInML}\n" +
            $"GeneralFeel: {GeneralFeel}\n" +
            $"Tierness: {Tierness}\n" +
            $"TodaysAchivments: {TodaysAchivments}";
    }
}
