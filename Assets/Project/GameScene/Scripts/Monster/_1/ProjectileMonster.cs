using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMonster : Monster
{
    public GameObject projectilePrefab;
    bool isBattle;
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
        BattleCheck();
        ContactCheck();
    }
    public virtual void BattleCheck() // 배틀 감지
    {
        RaycastHit2D rayhit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.6f), Vector2.left, AttackDistance, LayerMask.GetMask("Player"));
        if (rayhit.collider != null)
        {
            isBattle = true;
            TryAttack();
        }
        else
        {
            isBattle = false;
        }


    }

    public override void Move()
    {

        
            if (!BattleManager.Instance.isContact && !isBattle && !Stop || BattleManager.Instance.isContact && !isBattle && !Stop)
            {
                if (!BattleManager.Instance.isUseSkill)
                {
                    transform.Translate(Vector2.left * applySpeed * Time.deltaTime);
                }

            }
        else if (isBattle && !BattleManager.Instance.isContact && !Stop)
            {

            if (!BattleManager.Instance.isUseSkill)
            {
                transform.Translate(Vector2.left * 5 * Time.deltaTime);
            }
        }
        

    }

    public override void animUpdate()
    {
        anim.SetBool("isContact", isBattle);

    }
    public virtual void Create_projectile()
    {
        var obj = Instantiate(projectilePrefab, new Vector2(transform.position.x,transform.position.y + 0.2f), transform.rotation);
        obj.GetComponent<MonsterProjectile>().StrikingPower = Power;
    }
}
