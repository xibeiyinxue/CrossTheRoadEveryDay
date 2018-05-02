using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : RoadBase
{

    void Start()
    {
        base.Start();
        m_SetObj = new GameObject(this.gameObject.name).transform;
        m_SetObj.SetParent(GameObject.Find(this.gameObject.name).transform);
    }

    protected override void Init()
    {
        m_Time = 5; /*使游戏开始时就生成一辆车*/
        m_GeneratedTime = Random.Range(3, 6);
        m_Index = Random.Range(0, prefab.Length);

        p_ObjSpeed = Random.Range(1.5f, 5.5f);
        p_Left = (Random.Range(-2, 2) >= 0) ? true : false; /*该行代码负责决定该车道的车辆是向左还是向右*/
    }

    protected override GameObject GeneratingMethod() /*生成方法*/
    {
        GameObject interactiveObj = Instantiate(prefab[m_Index], prefab[m_Index].transform.position, prefab[m_Index].transform.rotation);
        AssignmentMethod(interactiveObj);
        m_Time = 0;
        return interactiveObj;
    }

    private void AssignmentMethod(GameObject obj)
    {
        obj.GetComponent<InteractiveBase>().Speed = p_ObjSpeed;
        obj.GetComponent<InteractiveBase>().Left = p_Left;
        obj.GetComponent<InteractiveBase>().Damage = 5;
        obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, -0.1f + transform.position.z);
        obj.transform.SetParent(m_SetObj);
        m_CreatObj.Add(obj);
    }
}
