using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour {

    [SerializeField]
    private Vector3 offset = Vector3.zero;

    private float m_MoveSpeed = 0;

    private Transform target = null;

    private float _moveTime = 0;
    public float MoveTime { get { return _moveTime; } set { _moveTime = value; } }

	void Start () {
		
	}

    private void LateUpdate()
    {
        if (GameManager.Instance.m_Player)
        {
            target = GameManager.Instance.m_Player.transform;
        }
        else
        {
            return;
        }

        WaitPlayerMove();
    }

    private void WaitPlayerMove()
    {
        float distance = (transform.position - (target.position - offset)).magnitude;
        transform.position = Vector3.Lerp(transform.position, target.position - offset, distance * Time.deltaTime);
    }
}
