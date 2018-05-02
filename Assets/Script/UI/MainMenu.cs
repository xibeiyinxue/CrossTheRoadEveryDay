using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    private CanvasGroup mainMenuGroup = null;
    [SerializeField]
    private CanvasGroup audioSetGroup = null;
    [SerializeField]
    private CanvasGroup leaderboardsGroup = null;

    private Stack<CanvasGroup> canvasGroupStack = new Stack<CanvasGroup>();
    private List<CanvasGroup> canvasGroupList = new List<CanvasGroup>();

    void Start () {
        UIManager.Instance.FaderOn(false, 1);

        canvasGroupList.Add(mainMenuGroup);
        canvasGroupList.Add(audioSetGroup);
        canvasGroupList.Add(leaderboardsGroup);

        canvasGroupStack.Push(mainMenuGroup);
        DisPlayMenu();
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
	}

    public void StartButton(string loadSceneName)
    {
        UIManager.Instance.FaderOn(true, 1f);
        StartCoroutine(StartLevel(loadSceneName));
    }

    public void AudioSetButton()
    {
        canvasGroupStack.Push(audioSetGroup);
        DisPlayMenu();
    }

    public void LeaderboardsButton()
    {
        canvasGroupStack.Push(leaderboardsGroup);
        DisPlayMenu();
    }

    public void Back()
    {
        if (canvasGroupStack.Count <= 1) return;

        canvasGroupStack.Pop();
        DisPlayMenu();
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
