using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : Singleton<AdsManager>
{
    private const string ANDROID_AD_ID = "4051169";
    private bool testMode = true;
    private string bannerID = "banner";

    private void Start()
    {
        Advertisement.Initialize(ANDROID_AD_ID, testMode);

        StartCoroutine(ShowBannerWhenInitialized());
    }

    private IEnumerator ShowBannerWhenInitialized()
    {
        if(PlayerPrefs.GetInt("AdsRemoved") != 1)
        {
            while (!Advertisement.isInitialized)
            {
                yield return new WaitForSeconds(0.5f);
            }

            Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);

            Advertisement.Show(bannerID);
        }       
    }
}
