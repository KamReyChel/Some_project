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

    // Start is called before the first frame update
    void Start()
    {
        yesButton.onClick.AddListener(delegate { Application.Quit(); });
        noButton.onClick.AddListener(delegate { SetPanelVisible(false); });

        SetPanelVisible(false);

        GameplayManager.OnGamePlaying += OnPlaying;
    }

    public void SetPanelVisible(bool visible)
    {
        panel.SetActive(visible);
    }

    public void OnPlaying()
    {
        SetPanelVisible(false);
    }
}
