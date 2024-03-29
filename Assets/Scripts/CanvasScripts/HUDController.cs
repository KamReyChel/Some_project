﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class HUDController : MonoBehaviour
{
    [SerializeField]
    private Button pauseButton;

    [SerializeField]
    private Button resetButton;

    [SerializeField]
    private Button loadButton;

    [SerializeField]
    private Button loadSceneButton;

    [SerializeField]
    private TMPro.TextMeshProUGUI pointsText;

    [SerializeField]
    private Slider progressBar;

    [SerializeField]
    private float timeToDone;

    [SerializeField] private Sprite[] loadButtonSprites;

    private const float TOLERANCE = 0.0001f;

    public delegate void SetSomeAssets();

    public static event SetSomeAssets testAssetSetting;

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

        loadButton.onClick.AddListener(delegate
        {
            testAssetSetting?.Invoke();
        });
        loadSceneButton.onClick.AddListener(delegate
        {
            AssetBundlesManager.Instance.LoadAbScene(0);
        });
        
        StartCoroutine(FillingTheSlider());

        loadSceneButton.image.sprite = loadButtonSprites[AnalyticsManager.Instance.GetIntParameter("SpriteColorIndex")];
    }

    /*
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Q))
            StartCoroutine(FillingTheSlider());
    }
    */
    
    
    public void UpdatePoints(int points)
    {
        pointsText.text = "Points: " + points;
    }
    
    public void UpdateFPSCount(float fps)
    {
        Debug.Log($"FPS: {fps}");
    }

    public void SetPauseActivation (bool activation)
    {
        pauseButton.gameObject.SetActive(activation);
    }

    public void SetResetActivation (bool activation)
    {
        resetButton.gameObject.SetActive(activation);
    }

    
    private IEnumerator FillingTheSlider()
    {
        float coroutineStart = Time.time;
        
        while (timeToDone != 0.0f)
        {
            if (!(Math.Abs(progressBar.value - 1.0f) < TOLERANCE))
            {
                progressBar.value = (Time.time - coroutineStart) / timeToDone;
            }
            
            yield return null;
        }
    }
}
