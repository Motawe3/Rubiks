using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueGameButton : MonoBehaviour
{
    [SerializeField] private Button continueButton;

    private void Start()
    {
        continueButton.onClick.AddListener(LoadGame);
        EnableContinueButton(CubeSaverLoader.HasGameSaved());
    }

    private void LoadGame()
    {
        GameManager.Instance.LoadSavedGame();
    }

    public void EnableContinueButton(bool isEnabled)
    {
        continueButton.interactable = isEnabled;
    }
}
