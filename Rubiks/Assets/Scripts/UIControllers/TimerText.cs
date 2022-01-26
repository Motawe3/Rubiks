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
        timerText.text = TimeManager.Instance.GetFormatedTime();
    }
}
