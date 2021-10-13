using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 攻击碰撞器C : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        FightInterface fi = other.GetComponent<FightInterface>();
        if(fi != null)
        {
            fi.beHit(10);
        }
    }
}
