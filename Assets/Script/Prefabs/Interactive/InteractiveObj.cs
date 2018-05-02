using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObj : InteractiveBase {

	void Start () {
        if (Left)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        initVec3 = transform.position;
        DeathPoint = transform.position.x;
	}

    protected override void Move()
    {
        if (Left)
        {
            transform.Translate(Vector2.left * Time.deltaTime * Speed);
            if (transform.position.x <= -DeathPoint)
            {
                DestorySelf();
            }
        }
        else
        {
            transform.Translate(Vector2.right * Time.deltaTime * Speed);
            if (transform.position.x >= -DeathPoint)
            {
                DestorySelf();
            }
        }
    }

    //protected override void Init()
    //{
    //    Speed = 0;
    //    Left = false;
    //    transform.position = initVec3;
    //}
}
