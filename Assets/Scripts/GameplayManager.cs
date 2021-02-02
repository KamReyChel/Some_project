using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EGameState
{
    Playing,
    Paused
}

public class GameplayManager : Singleton<GameplayManager>
{
    private EGameState m_state;

    List<IRestartableObject> m_restartableObjects = new List<IRestartableObject>();

    public EGameState GameState
    {
        get { return m_state; }

        set { 
            m_state = value;
            
            switch (m_state)
            {
                case EGameState.Paused:
                    {
                        if (OnGamePaused != null)
                            OnGamePaused();
                    }
                    break;

                case EGameState.Playing:
                    {
                        if (OnGamePlaying != null)
                            OnGamePlaying();
                    }
                    break;
            }
        }
    }

    public delegate void GameStateCallback();

    public static event GameStateCallback OnGamePaused;
    public static event GameStateCallback OnGamePlaying;
    public static event GameStateCallback GameReset;

    private HUDController m_HUD;
    private int m_points = 0;

    public int Points
    {
        get { return m_points; }
        set
        {
            m_points = value;
            m_HUD.UpdatePoints(m_points);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_state = EGameState.Playing;
        GetAllRestartableObjects();

        m_HUD = FindObjectOfType<HUDController>();
        Points = 0;

        OnGamePaused += DeactiveHUB;
        OnGamePlaying += ActiveHUB;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            PlayPause();
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GameState = EGameState.Paused;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            Restart();
        }

    }

    private void GetAllRestartableObjects()
    {
        m_restartableObjects.Clear();

        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (var rootGameObject in rootGameObjects)
        {
            IRestartableObject[] childrenInterfaces = rootGameObject.GetComponentsInChildren<IRestartableObject>();

            foreach(var childInterface in childrenInterfaces)
            {
                m_restartableObjects.Add(childInterface);
            }
        }
    }

    public void Restart()
    {
        foreach (var restartableObject in m_restartableObjects)
            restartableObject.DoRestart();

        Points = 0;

        if (GameReset != null)
            GameReset();
    }

    public void PlayPause()
    {
        switch (GameState)
        {
            case EGameState.Playing:
                {
                    GameState = EGameState.Paused;
                }
                break;

            case EGameState.Paused:
                {
                    GameState = EGameState.Playing;
                }
                break;
        }
    }

    private void ActiveHUB()
    {
        m_HUD.SetPauseActivation(true);
        m_HUD.SetResetActivation(true);
    }

    private void DeactiveHUB()
    {
        m_HUD.SetPauseActivation(false);
        m_HUD.SetResetActivation(false);
    }
}
