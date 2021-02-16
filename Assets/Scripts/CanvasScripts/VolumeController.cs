using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField]
    private Button volumeUp;

    [SerializeField]
    private Button volumeDown;

    [SerializeField]
    private Image bar;

    // Start is called before the first frame update
    void Start()
    {
        volumeUp.onClick.AddListener(delegate { OnChangeVolume(true); });
        volumeDown.onClick.AddListener(delegate { OnChangeVolume(false); });
    }

    private void UpdateBar()
    {
        bar.fillAmount = AudioListener.volume;
    }

    private void OnEnable()
    {
        UpdateBar();
    }

    private void OnChangeVolume(bool bUp)
    {
        float newValue = AudioListener.volume;

        if (bUp)
        {
            newValue += 0.1f;
        }
        else
        {
            newValue -= 0.1f;
        }

        newValue = Mathf.Clamp01(newValue);
        AudioListener.volume = newValue;
        UpdateBar();
    }

}
