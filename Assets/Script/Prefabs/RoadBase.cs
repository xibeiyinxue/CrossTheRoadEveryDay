using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBase : MonoBehaviour {

    [SerializeField]
    protected GameObject[] prefab = null;

    protected float ganeratedTime = 0;
    protected float objSpeed = 0;

    protected float m_Time = 0;
    protected int m_Index = 0;
    protected int m_Count = 0;

    protected float m_GeneratedTime = 0;

    protected float p_ObjSpeed = 0; /*Prefab_ObjSpeed*/
    protected bool p_Left = false; /*Prefab_ObjSpeed*/

    protected List<GameObject> m_CreatObj = new List<GameObject>();

    protected Transform m_SetObj;

	protected void Start () {
        Init();
	}
	
	void Update () {
        m_Time += Time.deltaTime;
        if (m_GeneratedTime <= m_Time)
        {
            GeneratingMethod();
        }
	}

    protected virtual void Init() { }

    protected virtual GameObject GeneratingMethod()
    {
        return null;
    }

    public void DestroySelf()
    {
        //yield return new WaitForSeconds(1f);
        if (m_CreatObj.Count > 0)
        {
            foreach (GameObject item in m_CreatObj)
            {
                Destroy(item);
            }
            m_CreatObj.Clear();
        }
        Destroy(this.gameObject);
    }
}
