using Gameplay;
using Gameplay.Controllers;
using TMPro;
using UnityEngine;
using Zenject;

public class GameOverUI : MonoBehaviour
{

    [SerializeField]
    private GameObject _content;

    [SerializeField]
    private TextMeshProUGUI _orderDeliveredNumberText;

    [Inject] private KitchenGameManager _kitchenGameManager;

    [Inject] private DeliveryManager _deliveryManager;

    private void Awake()
    {
        _kitchenGameManager.OnStateChanged += OnStateChangedHandle;
        Hide();
    }

    private void OnStateChangedHandle(KitchenGameManagerState newState)
    {
        if (newState == KitchenGameManagerState.GameOver)
            Show();
        else
            Hide();
    }

    private void Show()
    {
        _content.SetActive(true);
        _orderDeliveredNumberText.text = _deliveryManager.SuccessDeliveryCount.ToString();
    }

    private void Hide()
    {
        _content.SetActive(false);
    }


}