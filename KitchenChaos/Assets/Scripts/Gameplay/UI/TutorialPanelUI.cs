using System.Collections.Generic;
using DefaultNamespace;
using Gameplay;
using Zenject;

public class TutorialPanelUI : BaseContentUI
{

    private List<TutorialKeyUI> _tutorialKeyUis;

    [Inject] private GameInputManager _gameInputManager;
    [Inject] private KitchenGameManager _kitchenGameManager;

    private void Awake()
    {
        InitKeyUIs();
    }

    private void InitKeyUIs()
    {
        _tutorialKeyUis = new List<TutorialKeyUI>(GetComponentsInChildren<TutorialKeyUI>());
    }

    private void Start()
    {
        _gameInputManager.OnKeybindRebind += UpdateVisual;
        _kitchenGameManager.OnStateChanged += CheckForTutorailPassed;
        UpdateVisual();
        Show();
    }

    private void UpdateVisual()
    {
        foreach (var tutorialKey in _tutorialKeyUis)
        {
            tutorialKey.Text.text = _gameInputManager.GetBindingText(tutorialKey.Type);
        }
    }

    private void CheckForTutorailPassed(KitchenGameManagerState state)
    {
        if (state == KitchenGameManagerState.CountdownToStart)
            Hide();
    }
}