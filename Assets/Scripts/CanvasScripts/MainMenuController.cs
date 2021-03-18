using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button optionsButton;

    [SerializeField]
    private Button quitButton;

    [SerializeField]
    private Button removeAds;

    [SerializeField]
    private GameObject mainPanel;

    [SerializeField]
    private GameObject mainMenuPanel;

    [SerializeField]
    private GameObject optionsPanel;



    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener( delegate { OnPlay(); });
        optionsButton.onClick.AddListener( delegate { ShowOptions(true); });
        quitButton.onClick.AddListener( delegate { OnQuit(); });
        removeAds.onClick.AddListener(delegate { PurchasingManager.Instance.BuyRemoveAds();});
    }


    public void SetPanelVisible(bool visible)
    {
        mainPanel.SetActive(visible);

        if (PlayerPrefs.GetInt("AdsRemoved") == 1)
        {
            SetButtonRemoveAdsVisible(false);
        }

    }

    public void SetButtonRemoveAdsVisible(bool visible)
    {
        removeAds.gameObject.SetActive(visible);
    }

    private void OnPlay()
    {
        SetPanelVisible(false);
    }

    public void ShowOptions(bool visible)
    {
        optionsPanel.SetActive(visible);
        mainMenuPanel.SetActive(!visible);
    }

    private void OnQuit()
    {
        Application.Quit();
    }


}
