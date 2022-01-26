using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConfirmationPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text confirmText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    public void Initialize(string message, UnityAction action)
    {
        confirmText.text = message;
        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(action);
        confirmButton.onClick.AddListener(HidePopup);
        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(HidePopup);
        gameObject.SetActive(true);
    }
    
    public void HidePopup()
    {
        gameObject.SetActive(false);
    }
}
