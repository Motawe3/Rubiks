using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class CubePreviewImage : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> cubePreviews;
    [SerializeField]
    private Image previewImage;
    
    public void SetPreview(int previewSize)
    {
        previewImage.sprite = cubePreviews.Find(x => x.name == previewSize.ToString());
    }
}
