using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDirector : Singleton<LevelDirector> {

    private Player m_Player = null;
    private PlayerData data = null;

    private int m_Record = 0;
    private bool _creatRoad = false;
    public bool CreatRoad { get { return _creatRoad; } set { _creatRoad = value; } }

    public Player CurrentPlayer { get; private set; }
    private int _score;
    private int _maxScroe;
    public int Score {
        get {
            return _score;
        }
        set {
            _score = value;
            if (_maxScroe < _score)
            {
                data.maxScore = value;_maxScroe = value;
            }
        }
    }
    public int MaxScore { get { return _maxScroe; } }

    protected override void Awake()
    {
        Init();
    }

    void Start () {
        CreatPlayer();
        UIManager.instance.FaderOn(false, 1);
    }
	
	void Update () {
        if (CurrentPlayer == null) return;
        else
        {
            Score = CurrentPlayer.RecordFollowers;
            if (m_Record + 5 <= CurrentPlayer.RecordFollowers && !_creatRoad)
            {
                m_Record = CurrentPlayer.RecordFollowers;
                _creatRoad = true;
            }
        }

	}

    private void Init()
    {
        m_Player = Resources.Load<Player>("Prefabs/Player");
        data = Resources.Load<PlayerData>("PlayerData");
        _maxScroe = data.maxScore; 
    }

    private void CreatPlayer()
    {
        CurrentPlayer = Instantiate(m_Player, m_Player.transform.position, Quaternion.identity);
        GameManager.Instance.m_Player = CurrentPlayer;
        m_Record = m_Player.RecordFollowers;
    }

    public void OnGameOver()
    {
        GameManager.Instance.GameOverBool = true;
        AddHistoryScore();
    }

    private void AddHistoryScore()
    {
        if (Score <= 0) return;

        if (data.LeaderboardDatas.Count >= 10)
        {
            for (int i = 0; i < data.LeaderboardDatas.Count; i++)
            {
                if (Score > data.LeaderboardDatas[i].score)
                {
                    LeaderboardData leaderboardData = new LeaderboardData();
                    leaderboardData.score = _score;
                    leaderboardData.name = data.playerName;
                    data.LeaderboardDatas.Add(leaderboardData);
                }
            }
        }
        else
        {
            LeaderboardData leaderboadrData = new LeaderboardData();
            leaderboadrData.score = _score;
            leaderboadrData.name = data.playerName;
            data.LeaderboardDatas.Add(leaderboadrData);
        }
    }
}
