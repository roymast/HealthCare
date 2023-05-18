using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class CalendarDay : MonoBehaviour
{
    public static System.Action<DayData> OnCalendarDayClick;
    [SerializeField] TextMeshProUGUI DayLabel;
    [SerializeField] Button DayButton;
    [SerializeField] Image DayImage;
    DayData dayData;

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
        this.dayData = dayData;
        DayButton.onClick.AddListener(OnClickEvent);
    }

    void OnClickEvent()
    {
        OnCalendarDayClick?.Invoke(dayData);
    }

    void FillNullDay()
    {
        DayLabel.text = "";
        DayButton.interactable = false;        
    }
}
