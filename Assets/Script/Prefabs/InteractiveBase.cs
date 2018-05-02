using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBase : MonoBehaviour {

    protected Vector3 initVec3; /*初始位置*/
    protected float DeathPoint; /*死亡的点*/

    private float _speed = 0f;
    public float Speed { get { return _speed; } set { _speed = value; } }
    private bool _left = false;
    public bool Left { get { return _left; }set { _left = value; } }
    private int _damage = 0;
    public int Damage { get { return _damage; } set { _damage = value; } }

	void Start () {
		
	}
	

	void Update () {
        Move();
	}

    protected virtual void Move() { }

    protected virtual void Init() { }

    public void DestorySelf()
    {
        //Init();
        //this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ICanTakeDamage>() != null)
        {
            other.gameObject.GetComponent<ICanTakeDamage>().Damage(Damage, this.gameObject);
        }
    }
}
