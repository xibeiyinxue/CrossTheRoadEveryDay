using System.Collections.Generic;
using UnityEngine;
using System;

//在上方菜单栏中新建一个玩家的信息存档
[CreateAssetMenu(fileName = "PlayerData", menuName = "CreateScriptable/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public string playerName;
    public int maxScore;
    public List<LeaderboardData> LeaderboardDatas = new List<LeaderboardData>();
}

[Serializable]
public struct LeaderboardData : IComparable<LeaderboardData>
{
    public int score;
    public string name;

    public int CompareTo(LeaderboardData x)
    {
        return score.CompareTo(x.score);
    }
}
