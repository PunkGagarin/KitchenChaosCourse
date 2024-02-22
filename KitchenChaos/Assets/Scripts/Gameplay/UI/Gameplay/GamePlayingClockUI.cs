using Gameplay;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GamePlayingClockUI : MonoBehaviour
{

    [SerializeField]
    private Image _clockImage;

    [Inject] private KitchenGameManager _gameManager;


    private void Update()
    {
        _clockImage.fillAmount = _gameManager.GetGameTimerNormalized();
    }
}