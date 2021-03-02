using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : Singleton<SaveManager>
{
    //private float m_overallTime = 0.0f;

    private string m_pathBin;
    private string m_pathJSON;


    public GameSaveData saveData;

    public bool useBinary = true;

    private void Start()
    {
        m_pathBin = Path.Combine(Application.persistentDataPath, "save.bin");
        m_pathJSON = Path.Combine(Application.persistentDataPath, "save.json");

        //saveData.m_overallTime = 0.0f;
        //saveData.m_timeSinceLastTime = 0.0f;
        saveData.m_masterVolume = AudioListener.volume;
        LoadSettings();

        //Debug.Log(Application.persistentDataPath);
    }

    private void Update()
    {
        saveData.m_timeSinceLastTime += Time.deltaTime;
    }

    public void SaveSettings()
    {


        saveData.m_overallTime += saveData.m_timeSinceLastTime;
        Debug.Log("Saving overall time value: " + saveData.m_overallTime);
        Debug.Log("Points lifetime hits:" + saveData.m_lifetimeHits);

        //PlayerPrefs.SetInt("LifetimeHits", GameplayManager.Instance.LifetimeHits);
        //PlayerPrefs.SetFloat("OverallTime", saveData.m_overallTime);

        if (useBinary)
        {
            FileStream file = new FileStream(m_pathBin, FileMode.OpenOrCreate);
            BinaryFormatter binFormat = new BinaryFormatter();
            binFormat.Serialize(file, saveData);
            file.Close();
        }
        else
        {
            string saveData = JsonUtility.ToJson(this.saveData);
            File.WriteAllText(m_pathJSON, saveData);

        }

        saveData.m_timeSinceLastTime = 0.0f;
    }

    public void LoadSettings()
    {
        if(useBinary && File.Exists(m_pathBin))
        {
            FileStream file = new FileStream(m_pathBin, FileMode.Open);
            BinaryFormatter binFormat = new BinaryFormatter();
            saveData = (GameSaveData)binFormat.Deserialize(file);
            file.Close();
            ApplySettings();
        }
        else if(!useBinary && File.Exists(m_pathJSON))
        {
            string saveData = File.ReadAllText(m_pathJSON);
            this.saveData = JsonUtility.FromJson<GameSaveData>(saveData);
            ApplySettings();
        }
        else
        {
            saveData.m_timeSinceLastTime = 0.0f;
            saveData.m_overallTime = 0.0f;
            saveData.m_timeSinceLastTime = 0;
            saveData.m_masterVolume = AudioListener.volume;
        }

        //m_overallTime = PlayerPrefs.GetFloat("OverallTime", 0.0f);
        //GameplayManager.Instance.LifetimeHits = PlayerPrefs.GetInt("LifetimeHits", 0);
        //Debug.Log("Loaded overall time value: " + saveData.m_overallTime);
        //Debug.Log("Loaded lifetime hits value: " + saveData.m_lifetimeHits);
    }

    [Serializable]
    public struct GameSaveData
    {
        public float m_timeSinceLastTime;
        public float m_overallTime;
        public int m_lifetimeHits;
        public float m_masterVolume;
    }

    private void ApplySettings()
    {
        AudioListener.volume = saveData.m_masterVolume;
    }
}
