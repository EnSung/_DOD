using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{

    public float Power;
    void Attack()
    {
        BattleManager.Instance.PlayerDamage(Power);
        Invoke("Des",1.5f);
    }

    void Des()
    {
        Destroy(gameObject);
    }
}
