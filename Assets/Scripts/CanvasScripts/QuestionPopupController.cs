using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPopupController : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private Button yesButton;

    [SerializeField]
    private Button noButton;

    [SerializeField]
    MainMenuController menuController;

    [SerializeField]
    PauseMenuController pauseMenuController;



    // Start is called before the first frame update
    void Start()
    {
        yesButton.onClick.AddListener(delegate { ReturnToMainMenu(); });
        noButton.onClick.AddListener(delegate { SetPanelVisible(false); });

        SetPanelVisible(false);

        GameplayManager.OnGamePlaying += HidePanel;
    }

    public void SetPanelVisible(bool visible)
    {
        panel.SetActive(visible);
    }

    public void HidePanel()
    {
        SetPanelVisible(false);
    }

    public void ReturnToMainMenu()
    {
        GameplayManager.Instance.Restart();
        HidePanel();
        GameplayManager.Instance.GameState = EGameState.Playing;
        menuController.SetPanelVisible(true);
        pauseMenuController.SetPanelVisible(false);

    }
}
