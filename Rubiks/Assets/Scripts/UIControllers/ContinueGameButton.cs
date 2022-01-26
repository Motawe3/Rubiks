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
    }

    private void LoadGame()
    {
        GameManager.Instance.LoadSavedGame();
    }

}
