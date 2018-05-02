using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green : RoadBase {

    private List<int> m_X = new List<int>();

    private void Awake()
    {
        for (int i = -8; i < 9; i++)
        {
            m_X.Add(i);
        }
    }

    void Start () {
        base.Start();
	}
	
	void Update () {
		
	}

    protected override void Init()
    {
        m_SetObj = new GameObject(this.gameObject.name).transform;
        m_SetObj.SetParent(GameObject.Find(this.gameObject.name).transform);
        m_Count = Random.Range(2, 6);
        for (int i = 0; i < m_X.Count; i++)
        {
            //按位数获取预置体对象池内静态物体列表内的预置体
            m_Index = Random.Range(0, prefab.Length);
            if (m_X[i] >= -2 && m_X[i] <= 2)
            {
                p_Left = (Random.Range(-4, 1) >= 0) ? true : false;
            }
            else
            {
                p_Left = (Random.Range(-2, 2) >= 0) ? true : false;
            }

            if (m_Count == 0)
            {
                return;
            }
            else if (i == 1)
            {
                m_Count--;
                GameObject staicObj = Instantiate(prefab[m_Index],prefab[m_Index].transform.position,prefab[m_Index].transform.rotation);
                AssignmentMethod(staicObj, m_X[0]);
            }
            else if (i == 2)
            {
                m_Count--;
                GameObject staticObj = Instantiate(prefab[m_Index], prefab[m_Index].transform.position, prefab[m_Index].transform.rotation);
                AssignmentMethod(staticObj, m_X[m_X.Count - 1]);
            }
            else if (p_Left)
            {
                m_Count--;
                GameObject staticObj = Instantiate(prefab[m_Index], prefab[m_Index].transform.position, prefab[m_Index].transform.rotation);
                AssignmentMethod(staticObj, m_X[i]);
            }
        }
    }

    private void AssignmentMethod(GameObject obj,int x)
    {
        if (x == 0 && transform.position.z == 0)
        {
            Destroy(obj);
        }
        else
        {
            obj.transform.position = new Vector3(x, obj.transform.position.y, transform.position.z);
        }
        obj.transform.SetParent(m_SetObj);
        m_CreatObj.Add(obj);
    }
}
