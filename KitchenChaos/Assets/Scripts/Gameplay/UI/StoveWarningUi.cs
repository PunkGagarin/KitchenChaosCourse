using System;
using UnityEngine;
using UnityEngine.UI;

public class StoveWarningUi : MonoBehaviour
{

    [SerializeField]
    private Image _warningImage;

    private void Start()
    {
        Hide();
    }

    public void Show()
    {
        _warningImage.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _warningImage.gameObject.SetActive(false);
    }
}