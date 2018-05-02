using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    private LevelDirector director;

    [SerializeField]
    private Text scoreText = null;

    void Start()
    {
        director = LevelDirector.Instance;
    }

    void Update()
    {
        scoreText.text = director.Score.ToString();
    }
}
