using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_SwordAura_Projectile : MonoBehaviour
{
    public Vector2 AttackRange;
    float Power;
    float moveSpeed;

    public void Init(float power, float speed)
    {
        Power = power;
        moveSpeed = speed;

    }

    private void Update()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        if(transform.position.x >= 6.81f)
        {
            Destroy(gameObject);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, AttackRange);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Monster")
        {
            
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x + 15, collision.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            collision.gameObject.GetComponent<Monster>().Damaged(Power);

            Destroy(gameObject);
        }
        else if(collision.tag == "immuneMonster")
        {
            collision.gameObject.GetComponent<Monster>().Damaged(Power);

            Destroy(gameObject);
        }


    }
}