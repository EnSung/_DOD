using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster : Monster
{

    public override void Start()
    {
        base.Start();
    }



    public override void Update()
    {
        base.Update();

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        ContactCheck();
    }

    public override void Move()
    {
        if (!Stop)
        {
            transform.Translate(Vector2.left * applySpeed * Time.deltaTime);
        }
    }
    public override void ContactCheck() // 공격 감지
    {
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.6f), Vector2.left);
        RaycastHit2D rayhit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.6f), Vector2.left, AttackDistance, LayerMask.GetMask("Player"));
        if (rayhit.collider != null)
        {

            Stop = true;
            TryAttack();
        }
        else
        {
            Stop = false;
        }
    }



    
}
