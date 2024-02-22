using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        
    }

    private void OnPlayButtonHandle()
    {
        SceneManager.LoadScene(1);
    }

    private void OnQuitButtonHandle()
    {
        Application.Quit();
    }
}
