using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOptionsController : MonoBehaviour
{
    [SerializeField]
    private Button acceptButton;

    [SerializeField]
    private Button cancelButton;

    [SerializeField]
    private MainMenuController mainMenu;

    private float m_initialVolume = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        acceptButton.onClick.AddListener(delegate { OnAccept(); });
        cancelButton.onClick.AddListener(delegate { OnCancel(); });
    }

    private void OnEnable()
    {
        m_initialVolume = AudioListener.volume;
    }

    private void OnAccept()
    {
        SaveManager.Instance.saveData.m_masterVolume = AudioListener.volume;
        SaveManager.Instance.SaveSettings();
        mainMenu.ShowOptions(false);
    }

    private void OnCancel()
    {
        AudioListener.volume = m_initialVolume;
        mainMenu.ShowOptions(false);
    }
}
