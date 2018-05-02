using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCollection : MonoBehaviour {

    [SerializeField]
    private GameObject[] sceneObj;
    private Queue<GameObject> m_Map = new Queue<GameObject>();
    private List<GameObject> m_OldMap = new List<GameObject>();

    private int m_RoadRandom=0;
    private int m_MapLenght = 35;
    private int m_Road1Count = 0;
    private int m_Road2Count = 0;
    private int m_LastRoadCount = 0;

    private Transform m_MapHolder;

    void Awake() {
        m_MapHolder = new GameObject("MapInit").transform;
    }

	void Start () {
        InitMap();
	}
	
	void Update () {
        if (LevelDirector.Instance.CreatRoad)
        {
            LevelDirector.Instance.CreatRoad = false;
            //StartCoroutine(CreatRoad());
            CreatRoad();
        }
	}

    private void InitMap()
    {
        int m_Safely = Random.Range(12, 18);
        m_Map.Clear();
        for (int i = 0; i < 35; i++)
        {
            if (m_Safely > 0)
            {
                m_Safely--;
                m_RoadRandom = 0;
            }
            else
            {
                GenerateNext();
            }
            GameObject road = Instantiate(sceneObj[m_RoadRandom], new Vector3(0, 0, -11 + i), sceneObj[m_RoadRandom].transform.rotation);
            road.name = road.name + i;
            road.transform.SetParent(m_MapHolder);
            m_Map.Enqueue(road);
        }
    }

    private void GenerateNext()
    {
        m_RoadRandom = Random.Range(0, sceneObj.Length);
        //意义无，只是为了进行判断，如果生成了 2 号路面，那么下一次将生成草坪
        if (m_LastRoadCount < m_Road2Count)
        {
            m_RoadRandom = 0;
            m_Road2Count = 0;
        }
        else
        {
            //如果当随机生成的路面为 1 号路面时， 1 号路面计数 ++
            if (m_RoadRandom == 1)
            {
                m_Road1Count++;
            }

            //如果当随机生成的路面为 2 号路面时， 2 号路面计数 ++
            else if (m_RoadRandom == 2)
            {
                m_Road2Count++;
            }

            //如果 1 号路面的计数大于上一路面的计数，则将现在的 1 号路面数赋给上一路面数
            if (m_LastRoadCount < m_Road1Count)
            {
                m_LastRoadCount = m_Road1Count;
            }

            //如果上一路面数不等于 0 时进行比较，如果上一路面数与 1 号路面数相等时，代表随机路面不等于 1 号路面，于是我们将强制让其生成 2号路面
            else if (m_LastRoadCount != 0 && m_LastRoadCount == m_Road1Count)
            {
                m_RoadRandom = 2;
                m_Road2Count++;
                m_LastRoadCount =
                    m_Road1Count = 0;
            }
        }
    }

    private void CreatRoad()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject oldRoad = m_Map.Dequeue();
            m_OldMap.Add(oldRoad);
            print("Old Road.Name = " + oldRoad);
            m_RoadRandom = Random.Range(0, sceneObj.Length);
            GenerateNext();
            GameObject road = Instantiate(sceneObj[m_RoadRandom], new Vector3(0, 0, -11 + m_MapLenght), sceneObj[m_RoadRandom].transform.rotation);
            road.name = road.name + m_MapLenght;
            road.transform.SetParent(m_MapHolder);
            m_MapLenght++;
            m_Map.Enqueue(road);
            print(road);
        }

        if (m_OldMap.Count > 0)
        {
            foreach (GameObject item in m_OldMap)
            {
                item.GetComponent<RoadBase>().DestroySelf();
            }
            m_OldMap.Clear();
        }
        //yield return new WaitForSeconds(1f);
        //for (int i = 0; i < 5; i++)
        //{
        //    //Destroy(m_Map.Dequeue());
        //    m_Map.Dequeue().GetComponent<RoadBase>().DestroySelf();
        //    //GameObject lowRoad = m_Map.Dequeue();
        //    //StartCoroutine(lowRoad.GetComponent<RoadBase>().DestroySelf());
        //}
    }
}
