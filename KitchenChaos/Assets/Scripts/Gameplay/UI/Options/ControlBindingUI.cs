using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Gameplay.UI.Options
{

    public class ControlBindingUI : MonoBehaviour
    {

        [Inject] private GameInputManager _gameInputManager;

        private Dictionary<KeybindType, KeyBindPanelUI> Panels { get; } = new();

        [SerializeField]
        private GameObject _pressKeyPanel;

        private void Awake()
        {
            InitKeybindPanels();
            SubscribeKeybindPanels();
        }

        private void Start()
        {
            UpdateKeyBindVisual();
        }

        private void InitKeybindPanels()
        {
            var panels = GetComponentsInChildren<KeyBindPanelUI>();
            foreach (var panel in panels)
            {
                Panels.Add(panel.KeybindType, panel);
            }
        }

        private void SubscribeKeybindPanels()
        {
            foreach (var keyBindPanelUI in Panels)
            {
                keyBindPanelUI.Value.Button.onClick.AddListener(() => RebindBinding(keyBindPanelUI.Value.KeybindType));
            }
        }

        private void RebindBinding(KeybindType type)
        {
            ShowPressKeyPanel();
            _gameInputManager.RebindBinding(type, () =>
            {
                UpdateKeyBindVisual();
                HidePressKeyPanel();
            });
        }

        private void ShowPressKeyPanel()
        {
            _pressKeyPanel.SetActive(true);
        }

        private void HidePressKeyPanel()
        {
            _pressKeyPanel.SetActive(false);
        }

        public void UpdateKeyBindVisual()
        {
            foreach (var panelUI in Panels)
            {
                panelUI.Value.Text.text = _gameInputManager.GetBindingText(panelUI.Value.KeybindType);
            }
        }
    }

}