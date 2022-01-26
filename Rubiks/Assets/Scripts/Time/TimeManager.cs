using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public double PlayTime { get; private set; }
    public bool IsCounting { get; private set; }
    
    #region Singleton

    private static TimeManager _instance;

    public static TimeManager Instance
    {
        get { return _instance; }
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion
    
    private void Start()
    {
        PlayTime = 0.0f;
        InteractionManager.Instance.OnInteractionAllowed += SetTimerStatus;
    }

    public void SetTimer(double time)
    {
        PlayTime = time;
    }

    private void SetTimerStatus(bool isEnabled)
    {
        IsCounting = isEnabled;
    }

    private void Update()
    {
        if (IsCounting)
            PlayTime += Time.deltaTime;
    }
}
