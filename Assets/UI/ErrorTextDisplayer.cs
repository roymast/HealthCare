using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorTextDisplayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Animator _animator;

    public void OnDisplayError(string text, string Trigger)
    {
        _text.text = text;
        _animator.SetTrigger(Trigger);
    }
    public void OnDisplayError(string text, string Trigger, Color color)
    {
        _text.color = color;
        OnDisplayError(text, Trigger);
    }
}
