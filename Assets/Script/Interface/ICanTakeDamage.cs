using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanTakeDamage{
    void Damage(int damage, GameObject initiator);
}
