using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField]
    private Button pauseButton;

    [SerializeField]
    private Button resetButton;

    [SerializeField]
    private TMPro.TextMeshProUGUI pointsText;

    private void Start()
    {
        pauseButton.onClick.AddListener(delegate
        {
            GameplayManager.Instance.PlayPause();
        });

        resetButton.onClick.AddListener(delegate
        {
            GameplayManager.Instance.Restart();
        });
    }

    public void UpdatePoints(int points)
    {
        pointsText.text = "Points: " + points;
    }

    public void SetPauseActivation (bool activation)
    {
        pauseButton.gameObject.SetActive(activation);
    }

    public void SetResetActivation (bool activation)
    {
        resetButton.gameObject.SetActive(activation);
    }
}
