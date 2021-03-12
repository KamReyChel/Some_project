using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AssetBundlesManager : Singleton<AssetBundlesManager>
{
    [SerializeField] private string assetBundleName;
    [SerializeField] private string assetBundleURL;
    [SerializeField] private uint abVersion;
    [SerializeField] private string abVersionURL;
    [SerializeField] private string abLocalPath;
    [SerializeField] private string abLocalPathScenes;


    private AssetBundle ab;
    private AssetBundle abScenes;
    
    private IEnumerator Start()
    {
        /*
        if (string.IsNullOrEmpty(assetBundleURL))
        { 
            StartCoroutine(LoadAssets());
        }
        else
        {
            StartCoroutine(LoadAssetsFromURL());
        }
        */
        //yield return StartCoroutine(GetAbVersion());
        //yield return StartCoroutine(LoadAssets(assetBundleName, result => ab = result));
        //yield return StartCoroutine(LoadAssetsFromURL());
        yield return StartCoroutine(LoadAssetsFromMemoryAsync(abLocalPath));
        yield return StartCoroutine(LoadScenesFromURL(abLocalPathScenes));
    }

    public Sprite GetSprite(string assetName)
    {
        return ab.LoadAsset<Sprite>(assetName);
    }

    public void LoadAbScene(int indexOfScene)
    {
        string[] scenePaths = abScenes.GetAllScenePaths();

        if(scenePaths.Length > indexOfScene)
        {
            SceneManager.LoadScene(scenePaths[indexOfScene], LoadSceneMode.Additive);
        }
    }

    private IEnumerator LoadAssets(string name, Action<AssetBundle> bundle)
    {
        AssetBundleCreateRequest abcr;

        string path = Path.Combine(Application.streamingAssetsPath, name);

        abcr = AssetBundle.LoadFromFileAsync(path);

        yield return abcr;

        bundle.Invoke(abcr.assetBundle);
        
        Debug.LogFormat(abcr.assetBundle == null ? "Failed to Load Asset Bundle : {0}" : "Asset Bundle {0} loaded", name);
    }

    private IEnumerator LoadAssetsFromURL()
    {
        UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(assetBundleURL, abVersion, 0);
        
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            ab = DownloadHandlerAssetBundle.GetContent(uwr);
        }
        
        Debug.Log(ab == null ? "Failed to download Asset Bundle" : "Asset Bundle downloaded");
        Debug.Log("Downloaded bytes : " + uwr.downloadedBytes);
    }
    
    private IEnumerator GetAbVersion()
    {
        UnityWebRequest uwr = UnityWebRequest.Get(abVersionURL);

        yield return uwr.SendWebRequest();

        if(uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            Debug.Log("Downloaded Version of Asset Bundle is : " + uwr.downloadHandler.text);
        }

        abVersion = uint.Parse(uwr.downloadHandler.text);
    }

    private IEnumerator LoadAssetsFromMemoryAsync(string path)
    {
        AssetBundleCreateRequest cr = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(path));

        yield return cr;

        AssetBundle asset = cr.assetBundle;

        if (asset == null)
        {
            Debug.Log("Loaded asset is not exist.");
        }
        else
        {
            Debug.Log("Asset was successfully loaded!");
            ab = asset;
        }
    }

    private IEnumerator LoadScenesFromURL(string path)
    {
        UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(path);

        yield return uwr.SendWebRequest();

        if(uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            abScenes = DownloadHandlerAssetBundle.GetContent(uwr);
        }

        if (abScenes == null)
        {
            Debug.Log("abScene is null");
        }
        else
        {
            Debug.Log("abScene is not null");
        }
    }
    
}
