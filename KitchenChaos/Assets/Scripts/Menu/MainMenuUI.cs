using Menu;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField]
    private Button _playButton;

    [SerializeField]
    private Button _quitButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButtonHandle);
        _quitButton.onClick.AddListener(OnQuitButtonHandle);

        Time.timeScale = 1f;
    }

    private void OnPlayButtonHandle()
    {
        Loader.Load(SceneNames.Game);
    }

    private void OnQuitButtonHandle()
    {
        Application.Quit();
    }
}