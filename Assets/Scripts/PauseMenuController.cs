using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private Button resumeButton;

    [SerializeField]
    private Button resetButton;

    [SerializeField]
    private Button quitButton;

    [SerializeField]
    private QuestionPopupController popupController;

    // Start is called before the first frame update
    void Start()
    {
        resumeButton.onClick.AddListener(delegate { OnResume(); });
        quitButton.onClick.AddListener(delegate { OnQuit(); });
        resetButton.onClick.AddListener(delegate { OnReset(); });

        SetPanelVisible(false);

        GameplayManager.OnGamePaused += OnPause;
    }

    public void SetPanelVisible(bool visible)
    {
        panel.SetActive(visible);
    }

    private void OnPause()
    {
        SetPanelVisible(true);
    }

    private void OnResume()
    {
        GameplayManager.Instance.GameState = EGameState.Playing;
        SetPanelVisible(false);
    }

    private void OnReset()
    {
        GameplayManager.Instance.GameState = EGameState.Playing;
        GameplayManager.Instance.Restart();
        SetPanelVisible(false);
    }

    private void OnQuit()
    {
        popupController.SetPanelVisible(true);
    }
}
