using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UIInput;

public class DayDataUI : MonoBehaviour
{
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
    [SerializeField] Button Apply;
}
