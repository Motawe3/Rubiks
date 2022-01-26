using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerText : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    
    private void Update()
    {
        if(!TimeManager.Instance.IsCounting) return;
        timerText.text = FormatTime(TimeManager.Instance.PlayTime);
    }

    public string FormatTime(double playTime)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds( playTime );
        return $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}:{timeSpan.Milliseconds:D3}";
    }
}
