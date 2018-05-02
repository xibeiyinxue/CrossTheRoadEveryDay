using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : RoadBase {

	void Start () {
        base.Start();
        m_SetObj = new GameObject(this.gameObject.name).transform;
        m_SetObj.SetParent(GameObject.Find(this.gameObject.name).transform);
	}

    protected override void Init()
    {
        m_Time = 5;
        m_GeneratedTime = Random.Range(8, 16);

        p_ObjSpeed = 13;
        p_Left = (Random.Range(-2, 2) >= 0) ? true : false;
    }

    protected override GameObject GeneratingMethod()
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
        obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, 0.04f + transform.position.z);
        obj.transform.SetParent(m_SetObj);
        m_CreatObj.Add(obj);
    }
}
