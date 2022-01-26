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
        undoButton.onClick.AddListener(PerformUndo);
        InteractionManager.Instance.OnInteractionAllowed += UpdateVisibility;
        CommandsHistoryManager.OnHistoryUpdated += UpdateUndoIcon;
        UpdateUndoIcon();
    }

    private void UpdateVisibility(bool isInteractive)
    {
        gameObject.SetActive(isInteractive);
    }

    private void PerformUndo()
    {
        if(!CubeSliceRotator.isCubeSliceRotating)
            CommandsHistoryManager.PopCommand();
    }

    void UpdateUndoIcon()
    {
        undoIcon.color = CommandsHistoryManager.HasCommands()? Color.white : Color.black;
    }
}
