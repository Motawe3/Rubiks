using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CubeSolvedPopup : MonoBehaviour
{
    [SerializeField] private Button titleScreenButton;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private TMP_Text cubeSizeDisplay;
    [SerializeField] private TMP_Text timeDisplay;
    
    private void Start()
    {
        titleScreenButton.onClick.AddListener(ExitGame);
        replayButton.onClick.AddListener(Replay);
        continueButton.onClick.AddListener(HideMenu);
        GameManager.Instance.OnGameWon += ShowPopup;
        HideMenu();
        
    }

    private void ShowPopup()
    {
        Invoke(nameof(ShowPopupDelayed) , 3.0f);
    }

    void ShowPopupDelayed()
    {
        cubeSizeDisplay.text = GameManager.Instance.currentCube3D.Size.ToString();
        timeDisplay.text = TimeManager.Instance.GetFormatedTime();
        gameObject.SetActive(true);
    }

    void Replay()
    {
        HideMenu();
        GameManager.Instance.StartNewGame(GameManager.Instance.currentCube3D.Size);
    }
    
    void ExitGame()
    {
        HideMenu();
        GameManager.Instance.SaveGame();
        GameManager.Instance.EndGame();
    }
    
    public void HideMenu()
    {
        gameObject.SetActive(false);
    }
}
