using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuttingProgressBarUI : MonoBehaviour
{

    [SerializeField]
    private Image _progressBar;

    public void SetImageProgressValue(float progressValue)
    {
        _progressBar.fillAmount = progressValue;
    }

    public void ResetFillAmount()
    {
        _progressBar.fillAmount = 0f;
    }
}