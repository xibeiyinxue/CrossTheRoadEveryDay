using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager> {

    public Player m_Player { get; set; }
    public int GameFrameRate = 300;
    public float TimeScale { get; private set; }
    private bool _paused;
    public bool Paused { get { return _paused; } set { _paused = value; } }
    private bool _gameOverBool;
    public bool GameOverBool { get { return _gameOverBool; } set { _gameOverBool = value; } }
    private bool _gameRelive;
    public bool GameRelive { get { return _gameRelive; } set { _gameRelive = value; } }

    private float savedTimeScale = 1f;

    public void Reset()
    {
        TimeScale = 1f;
        Paused = false;
        GameOverBool = false;
        UnPause();
    }

    public virtual void Pause()
    {
        if (Time.timeScale > 0.0f)
        {
            instance.SetTimeScale(0.0f);
            instance.Paused = true;
        }
        else
            UnPause();
    }

    public virtual void UnPause()
    {
        instance.ResetTimeScale();
        instance.Paused = false;
    }

    public void SetTimeScale(float newTimeScale)
    {
        savedTimeScale = Time.timeScale;
        Time.timeScale = newTimeScale;
    }

    public void ResetTimeScale()
    {
        Time.timeScale = savedTimeScale;
    }
}
