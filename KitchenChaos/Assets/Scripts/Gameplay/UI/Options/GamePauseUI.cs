using DefaultNamespace;
using Menu;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.UI.Options
{

    public class GamePauseUI : BaseContentUI
    {
        [Inject] private KitchenGameManager _kitchenGameManager;

        [SerializeField]
        private OptionsUI _optionsUI;

        [SerializeField]
        private Button _resumeButton;
    
        [SerializeField]
        private Button _optionsButton;

        [SerializeField]
        private Button _menuButton;
    
        private void Awake()
        {
            _resumeButton.onClick.AddListener(OnResumeClickHandle);
            _optionsButton.onClick.AddListener(OnOptionsClickHandle);
            _menuButton.onClick.AddListener(OnMenuClickHandle);
        }

        private void Start()
        {
            _kitchenGameManager.OnGamePaused += Show;
            _kitchenGameManager.OnGameUnpaused += Hide;
        }

        private void OnDestroy()
        {
            _resumeButton.onClick.RemoveListener(OnResumeClickHandle);
            _menuButton.onClick.RemoveListener(OnMenuClickHandle);
            _optionsButton.onClick.RemoveListener(OnOptionsClickHandle);
            _kitchenGameManager.OnGamePaused -= Show;
            _kitchenGameManager.OnGameUnpaused -= Hide;
        }

        private void OnResumeClickHandle()
        {
            _kitchenGameManager.ToggleGamePause();
        }

        private void OnMenuClickHandle()
        {
            Loader.Load(SceneNames.MainMenu);
        }
    
        private void OnOptionsClickHandle()
        {
            _optionsUI.Show(Show);
            base.Hide();    
        }

        public override void Show()
        {
            base.Show();
            _resumeButton.Select();
        }


        public override void Hide()
        {
            _optionsUI.Hide();
            base.Hide();
        }

    }

}