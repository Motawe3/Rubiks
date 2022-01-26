using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button titleScreenButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button timerToggleButton;
    [SerializeField] private Image timerToggleIcon;
    [SerializeField] private Button audioToggleButton;
    [SerializeField] private Image audioToggleIcon;
    [SerializeField] private GameObject timerHolder;
    [SerializeField] private ConfirmationPopup confirmationPopup;

    private void OnEnable()
    {
        GameManager.Instance.PauseGame(true);
    }
    
    private void OnDisable()
    {
        GameManager.Instance.PauseGame(false);
    }
    
    private void Start()
    {
        titleScreenButton.onClick.AddListener(TitleScreenClicked);
        continueButton.onClick.AddListener(HideMenu);
        replayButton.onClick.AddListener(ReplayClicked);
        timerToggleButton.onClick.AddListener(ToggleTimerClicked);
        audioToggleButton.onClick.AddListener(ToggleAudioClicked);
    }

    private void ReplayClicked()
    {
        confirmationPopup.Initialize("Are you sure you want to replay?" , Replay);
    }

    void Replay()
    {
        HideMenu();
        GameManager.Instance.StartNewGame(GameManager.Instance.currentCube3D.Size);
    }

    private void TitleScreenClicked()
    {
        confirmationPopup.Initialize("Are you sure you want to exit?" , ExitGame);
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
    
    private void ToggleTimerClicked()
    {
        timerHolder.SetActive(!timerHolder.activeInHierarchy);
        timerToggleIcon.color = timerHolder.activeInHierarchy? Color.white : Color.gray;
    }

    private void ToggleAudioClicked()
    {
        AudioManager.Instance.EnableAudio(!AudioManager.Instance.AudioEnabled);
        audioToggleIcon.color = AudioManager.Instance.AudioEnabled? Color.white : Color.gray;    
    }

}
