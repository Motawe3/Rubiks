using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSwitcher : MonoBehaviour
{
    [SerializeField] private CanvasGroup titleScreen;
    [SerializeField] private CanvasGroup gameScreen;
    [SerializeField] private float fadeSpeed = 5.0f;

    private void Start()
    {
        GameManager.Instance.OnGameStarted += SetGameScreen;
        GameManager.Instance.OnGameEnded += SetTitleScreen;
    }

    public void SetTitleScreen()
    {
        EnableScreen(titleScreen, true);
        EnableScreen(gameScreen, false);
    }

    public void SetGameScreen()
    {
        EnableScreen(titleScreen, false);
        EnableScreen(gameScreen, true);
    }

    private void EnableScreen(CanvasGroup screen, bool isEnabled)
    {
        StartCoroutine(FadeScreen(screen, isEnabled ? 1.0f : 0.0f));
        screen.blocksRaycasts = isEnabled;
        screen.interactable = isEnabled;
    }
    
    IEnumerator FadeScreen(CanvasGroup screen, float value)
    {
        while (Math.Abs(screen.alpha - value) > 0.05f)
        {
            screen.alpha = Mathf.Lerp(screen.alpha, value, Time.deltaTime * fadeSpeed);
            yield return null;
        }

        screen.alpha = value;
    }
}