using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    public abstract void triggerAggro(Transform target);
    public abstract void dmgKnockback(Transform source,float force = 0 ,int dmg = 0);
}
