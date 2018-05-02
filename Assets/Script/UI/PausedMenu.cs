using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PausedMenu : MonoBehaviour {

    [SerializeField]
    private AudioMixerSnapshot pausedAudio,unpausedAudio;
    [SerializeField]
    private CanvasGroup pauseGroup;
    [SerializeField]
    private CanvasGroup audioSetGroup;
    [SerializeField]
    private CanvasGroup gameOverGroup;

    private Stack<CanvasGroup> canvasGroupStack = new Stack<CanvasGroup>();
    private List<CanvasGroup> canvasGroupList = new List<CanvasGroup>();

	void Start () {
        canvasGroupList.Add(pauseGroup);
        canvasGroupList.Add(audioSetGroup);
        canvasGroupList.Add(gameOverGroup);
        DisPlayMenu();
	}

    private void Update()
    {
        if (GameManager.Instance.GameOverBool)
        {
            GameOverMenu();
        }
    }

    private void LowPass()
    {
        if (GameManager.Instance.TimeScale == 0)
        {
            pausedAudio.TransitionTo(.01f);
        }
        else
        {
            unpausedAudio.TransitionTo(.01f);
        }
    }

    public void Back()
    {
        if (canvasGroupStack.Count == 0)
        {
            Paused();
        }
        else
        {
            if (canvasGroupStack.Count > 0)
            {
                canvasGroupStack.Pop();
                if (canvasGroupStack.Count == 0)
                {
                    UnPaused();
                }
            }
        }
        DisPlayMenu();
    }

    public void Paused()
    {
        LowPass();
        GameManager.Instance.Pause();
        if (canvasGroupStack.Count> 0)
        {
            canvasGroupStack.Pop();
        }
        else
        {
            canvasGroupStack.Push(pauseGroup);
        }
        DisPlayMenu(); 
    }

    public void UnPaused()
    {
        LowPass();
        GameManager.Instance.Pause();
        if (canvasGroupStack.Count >0)
        {
            canvasGroupStack.Pop();
        }
        DisPlayMenu();
    }

    public void GameOverMenu()
    {
        LowPass();
        GameManager.Instance.Pause();
        canvasGroupStack.Push(gameOverGroup);
        DisPlayMenu();
    }

    public void AudioSetButton()
    {
        canvasGroupStack.Push(audioSetGroup);
        DisPlayMenu();
    }

    public void GameReplay(string loadSceneName)
    {
        LowPass();
        GameManager.Instance.UnPause();
        UIManager.Instance.FaderOn(true, 1);
        StartCoroutine(StartLevel(loadSceneName));
        GameManager.Instance.GameOverBool = false;
    }

    private IEnumerator StartLevel(string loadSceneName)
    {
        yield return new WaitForSeconds(1f);
        LoadSceneManager.LoadScene(loadSceneName);
    }

    private void DisPlayMenu()
    {
        foreach (var item in canvasGroupList)
        {
            item.alpha = 0;
            item.interactable = false;
            item.blocksRaycasts = false;
        }

        if (canvasGroupStack.Count > 0)
        {
            CanvasGroup cg = canvasGroupStack.Peek();
            cg.alpha = 1;
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }
    }
}
