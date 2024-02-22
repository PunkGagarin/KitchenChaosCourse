using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using TMPro;
using UnityEngine;
using Zenject;

public class GameplayStartCountdownUI : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI _countdownNumberText;

    [Inject] private KitchenGameManager _kitchenGameManager;


    private void Awake()
    {
        _kitchenGameManager.OnStateChanged += OnStateChangedHandle;
        Hide();
    }

    private void Update()
    {
        if (_countdownNumberText.gameObject.activeSelf)
            _countdownNumberText.text =  Mathf.Ceil( _kitchenGameManager.CountDownTimer).ToString();
    }

    private void OnStateChangedHandle(KitchenGameManagerState newState)
    {
        if (newState == KitchenGameManagerState.CountdownToStart)
            Show();
        else
            Hide();
    }

    private void Show()
    {
        _countdownNumberText.gameObject.SetActive(true);
    }

    private void Hide()
    {
        //todo: брать текст из ГеймМанагера
        _countdownNumberText.text = 3.ToString();
        _countdownNumberText.gameObject.SetActive(false);
    }
}