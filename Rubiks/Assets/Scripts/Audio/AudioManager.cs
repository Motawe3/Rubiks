using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   [SerializeField]
   private List<AudioClip> cubeRotationClips;

   [SerializeField]
   private AudioSource audioSource;

   private System.Random random = new System.Random();
   
   #region Singleton

   private static AudioManager _instance;

   public static AudioManager Instance
   {
      get { return _instance; }
   }


   private void Awake()
   {
      if (_instance != null && _instance != this)
      {
         Destroy(this.gameObject);
      }
      else
      {
         _instance = this;
      }
   }

   #endregion

   private void Start()
   {
      CubeSliceRotator.OnSliceRotationStarted += PlayRandomRotationClip;
   }

   public void PlayRandomRotationClip()
   {
      audioSource.PlayOneShot( cubeRotationClips[random.Next(0 , cubeRotationClips.Count)]);
   }
}
