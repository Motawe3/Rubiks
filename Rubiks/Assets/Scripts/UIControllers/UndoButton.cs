using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UndoButton : MonoBehaviour
{
    [SerializeField] private Button undoButton;
    [SerializeField] private Image undoIcon;

    void Start()
    {
        undoButton.onClick.AddListener(UndoRequested);
    }

    private void UndoRequested()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
