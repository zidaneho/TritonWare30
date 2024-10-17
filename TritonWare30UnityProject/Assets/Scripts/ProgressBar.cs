using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProgressBar : MonoBehaviour
{
    [SerializeField] private RectTransform progress;
    
    //Should the progress bar be horizontal, or vertical
    [SerializeField] private bool isHorizontal;


    public void SetProgress(float value, float maxValue)
    {
        if (progress == null)
        {
            Debug.LogWarning("Please set the progress transform in the inspector");
            return;
        }
        if (isHorizontal)
        {
            progress.localScale = new Vector3(value / maxValue, progress.localScale.y,progress.localScale.z);
        }
        else
        {
            progress.localScale = new Vector3(progress.localScale.x,value / maxValue,progress.localScale.z);
        }
    }
}
