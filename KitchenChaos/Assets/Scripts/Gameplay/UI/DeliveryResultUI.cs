using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DeliveryResultUI : MonoBehaviour
{

    private Animator _animator;

    [Inject] private DeliveryManager _deliveryManager;

    [SerializeField]
    private Image _backgroundImage;

    [SerializeField]
    private Image _iconImage;

    [SerializeField]
    private TextMeshProUGUI _text;

    [SerializeField]
    private Color _onSuccessColor;

    [SerializeField]
    private Color _onFailColor;

    [SerializeField]
    private Sprite _onSuccessSprite;

    [SerializeField]
    private Sprite _onFailSprite;

    private static readonly int Popup = Animator.StringToHash("Popup");


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    private void Start()
    {
        _deliveryManager.OnOrderCompleted += OnOrderCompleted;
        _deliveryManager.OnOrderFailed += OnOrderFailed;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _deliveryManager.OnOrderCompleted -= OnOrderCompleted;
        _deliveryManager.OnOrderFailed -= OnOrderFailed;
    }

    private void OnOrderCompleted(OrderCompletedEventArgs args)
    {
        gameObject.SetActive(true);
        _animator.SetTrigger(Popup);
        _backgroundImage.color = _onSuccessColor;
        _iconImage.sprite = _onSuccessSprite;
        _text.text = "DELIVERY\nSUCCESS";
    }

    private void OnOrderFailed(Transform transform)
    {
        gameObject.SetActive(true);
        _animator.SetTrigger(Popup);
        _backgroundImage.color = _onFailColor;
        _iconImage.sprite = _onFailSprite;
        _text.text = "DELIVERY\nFAILED";
    }

}