using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UIInput;
using UnityEngine.Events;

public class DayDataUI : MonoBehaviour
{
    public static System.Action<DayData> OnSave;
    DayData CurrentDayData;
    [SerializeField] UIInput.UIInput BreakFast;
    [SerializeField] UIInput.UIInput Lanch;
    [SerializeField] UIInput.UIInput Dinner;
    [SerializeField] UIInput.UIInput Night;
    List<TMP_InputField> Snacks;
    [SerializeField] UIInput.UIInput WaterInML;

    //List<WorkOut> workouts;
    [SerializeField] UIInput.UIInput Tierness;
    [SerializeField] UIInput.UIInput GeneralFeel;
    List<TMP_InputField> TodaysAchivments;

    List<TMP_InputField> outs;
    [SerializeField] Button Save;
    
    public void SetData(DayData dayData)
    {
        CurrentDayData = dayData;
        BreakFast.SetInput(dayData.BreakFast);
        BreakFast.SetLabel("BreakFast");
        Lanch.SetInput(dayData.Lanch);
        Lanch.SetLabel("Lanch");
        Dinner.SetInput(dayData.Dinner);
        Dinner.SetLabel("Dinner");        
        Night.SetInput(dayData.Night);
        Night.SetLabel("Night");
        WaterInML.SetInput(dayData.WaterInML.ToString());
        WaterInML.SetLabel("WaterInML");
        Tierness.SetInput(dayData.Tierness.ToString());
        Tierness.SetLabel("Tierness");
        GeneralFeel.SetInput(dayData.GeneralFeel);
        GeneralFeel.SetLabel("GeneralFeel");
    }
    public void OnSaveClick()
    {
        OnSave?.Invoke(GetData());
    }
    public DayData GetData()
    {
        CurrentDayData.BreakFast = BreakFast.GetINput();
        CurrentDayData.Lanch = Lanch.GetINput();
        CurrentDayData.Dinner = Dinner.GetINput();
        CurrentDayData.Night = Night.GetINput();
        CurrentDayData.WaterInML = int.Parse(WaterInML.GetINput());
        CurrentDayData.Tierness = int.Parse(Tierness.GetINput());
        CurrentDayData.GeneralFeel = GeneralFeel.GetINput();
        return CurrentDayData;
    }
}
