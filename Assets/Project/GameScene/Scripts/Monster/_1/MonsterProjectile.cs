using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{

    public float Speed;
    public float StrikingPower;
    void Start()
    {
        
    }

    void Update()
    {

        transform.Translate(Vector2.left * Speed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            BattleManager.Instance.PlayerDamage(StrikingPower);
            Destroy(gameObject);
        }
    }
}
