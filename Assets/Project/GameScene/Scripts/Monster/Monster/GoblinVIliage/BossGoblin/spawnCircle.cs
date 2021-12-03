using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnCircle : MonoBehaviour
{
    void Start()
    {
        Invoke("spawn", 1);
    }

    void Update()
    {
        
    }

    void spawn()
    {
        int index = Random.Range(20006,20009);
        var obj = Objectpool.GetMonsterobject(index, transform.position);
        obj.applyPower = obj.applyPower * 1.5f;
        obj.applyMaxHp = obj.applyMaxHp * 1.5f;
        obj.Hp = obj.applyMaxHp;
        Invoke("des", 0.8f);
    }

    void des()
    {
        Destroy(gameObject);
    }
}
