using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayData
{
    int year;
    int month;
    int day;    
    string BreakFast;
    string Lanch;
    string Dinner;
    string Night;
    List<string> Snacks;
    int WaterInML;

    //List<WorkOut> workouts;
    int Tierness;
    string GeneralFeel;
    List<string> TodaysAchivments;

    List<string> outs;

    public void SetData(int dayNum)
    {        
        day = dayNum;
    }

    public void InsertToSQL()
    {

    }
}
