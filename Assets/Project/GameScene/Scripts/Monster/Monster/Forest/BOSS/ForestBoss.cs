using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DOD.Define;
public class ForestBoss : BossMonster
{

    bool isBattle;
    public Character player;
    bool isAngry;

    public GameObject attackPrefab;
    public GameObject angryAttackPrefab;
    public override void Awake()
    {
        base.Awake();
        SetData((int)bossEnum.거대나무);
        Debug.Log("hp : " + Hp);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }


    public override void OnEnable()
    {
        base.OnEnable();
    }
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        CurrentAttackDelay += Time.deltaTime;

        if (Hp >= applyMaxHp)
        {
            Hp = applyMaxHp;
        }
        if (isApear)
        {
            SetHp(Hp, MaxHp);
        }
        Move();
        DieCheck();
        animUpdate();
        hpUpdate();
        angryCheck();
    }

    public override void FixedUpdate()
    {
        if (BattleManager.Instance.isEnd)
        {
            return;
        }

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

    public override void ContactCheck() // 공격 감지
    {
        if (BattleManager.Instance.isEnd)
        {
            return;
        }
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.6f), Vector2.left);
        RaycastHit2D rayhit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.6f), Vector2.left, 0.6f, LayerMask.GetMask("Player"));
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

    public override void Attack()
    {
        if (!isAngry)
        {
            var obj = Instantiate(attackPrefab, new Vector2(player.gameObject.transform.position.x, -0.1484008f), Quaternion.identity);

            obj.GetComponent<AttackObject>().Power = this.applyPower;
        }
        else
        {
            var obj = Instantiate(angryAttackPrefab, new Vector2(player.gameObject.transform.position.x, 0.6f), Quaternion.identity);

            obj.GetComponent<AttackObject>().Power = this.applyPower;
        }
    }

    public override void TryAttack()
    {
        base.TryAttack();
    }


    public override void Damaged(float value)
    {
        base.Damaged(value);
    }

    public override void Move()
    {


        if (!BattleManager.Instance.isContact)
        {
            if (!BattleManager.Instance.isUseSkill)
            {
                transform.Translate(Vector2.left * applySpeed * Time.deltaTime);
            }

        }


    }

    public void angryCheck()
    {
        if (Hp <= applyMaxHp / 2)
        {
            if (!isAngry)
            {
                isAngry = true;
                applyPower = applyPower * 2;
                anim.SetBool("isAngry", true);
                anim.SetTrigger("Angry");
                ApplyAttackDelay = AttackDelay / 2;
            }
        }
    }

    
}
