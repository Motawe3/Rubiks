using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudibleButton : MonoBehaviour , IPointerEnterHandler, IPointerClickHandler
{
    public static Action OnButtonHovered;
    public static Action OnButtonClicked;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnButtonHovered?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnButtonClicked?.Invoke();
    }
}
