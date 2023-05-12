using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CalendarDay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI DayLabel;
    [SerializeField] Button DayButton;
    [SerializeField] Image DayImage;

    public void InitDay(int day, DayData dayData, bool isToday=false)
    {
        if (day == 0)
            FillNullDay();
        else
            FillRegualrDay(day, dayData, isToday);        
    }

    private void FillRegualrDay(int day, DayData dayData, bool isToday=false)
    {
        DayLabel.text = day.ToString();
        if (isToday)
            DayImage.color = Color.green;
        else
            DayImage.color = Color.white;
    }

    void FillNullDay()
    {
        DayLabel.text = "";
        DayButton.interactable = false;        
    }
}
