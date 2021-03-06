using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAssetLoader : MonoBehaviour
{

    [SerializeField]
    private string spriteAssetName;

    [SerializeField] 
    private SpriteRenderer sr;

    private void Start()
    {
        HUDController.testAssetSetting += SetSpriteRendererAsset;
    }

    private void SetSpriteRendererAsset()
    {
        sr.sprite = AssetBundlesManager.Instance.GetSprite(spriteAssetName);
    }
}
