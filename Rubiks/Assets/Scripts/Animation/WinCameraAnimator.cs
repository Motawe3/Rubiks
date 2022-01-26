using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WinCameraAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    void Start()
    {
        GameManager.Instance.OnGameWon += PlayWiningAnimation;
        Hide();
    }

    void PlayWiningAnimation()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("Win");
        Invoke("Hide" , 3.0f);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
