using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DOD.DB;
using TMPro;

public abstract class Monster : Actor
{
    [Header("State")]
    public float Speed;
    public float applySpeed;
    public float AddScore;
    public int AddCoin;
    public int CoinAmount;

    public Transform effect_Pos;

    public bool Stop;
    public bool isDie = false;
    
    public GameObject CoinPrefab;
    public Slider Hpbar;
    public Transform DamageText_SpawnPoint;
    public GameObject canvas;


    public bool isBossGoblin;
    public override void Awake()
    {
        base.Awake();
        applySpeed = Speed;

    }
    public override void Start()
    {

        base.Start();

        if (Hpbar != null)
        {
            Hpbar.value = 1f;
        }
        applySpeed = Speed;
    }

    public virtual void OnEnable()
    {
        Hp = applyMaxHp;
        isDie = false;
    }

    public override void Update()
    {
        base.Update();
        DieCheck();
        HpUI_Update();
        Move();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void SetData(int Key)
    {
        var dict = DataBaseManager.Instance.tdMonsterDict[Key];
        unitNo = dict.unitNo;
        Name = dict.Name;
        MaxHp = dict.Hp;
        Power = dict.Power;
        Speed = dict.Speed;
        AttackDelay = dict.AttackDelay;
        AttackDistance = dict.AttackDistance;
        AddScore = dict.AddScore;
        AddCoin = dict.AddCoin;
        
    }
    public virtual void HpUI_Update()
    {
        Hpbar.value = Mathf.Lerp(Hpbar.value, Hp / MaxHp, 8f * Time.deltaTime);

    }


    public override void Damaged(float value)
    {
        base.Damaged(value);
        Objectpool.GetDamageText(canvas, DamageText_SpawnPoint.position, ((int)value).ToString());

        /*Vector2 pos = new Vector2(transform.position.x + 0.2f, transform.position.y);

        while(transform.position.x <= pos.x - 0.01f)
        {
            transform.position = Vector2.Lerp(transform.position, pos, 3 * Time.deltaTime);
        }*/
    }
    public abstract void Move();

    public override void DieCheck()
    {
        if (Hp <= 0)
        {
            anim.SetBool("isDie", true);
            if (!isDie)
            {
                Invoke("Die", 0.5f);
                isDie = true;
            }
        }
    }
    
    public virtual void Die()
    {
        GameSceneUIManager.Instance.GetScore(AddScore);
        for (int i = 0; i < 3; i++)
        {
            Objectpool.GetCoinobject(new Vector2(transform.position.x, transform.position.y + 0.5f));
        }

        BattleManager.Instance.GetGold(AddCoin);
        Objectpool.ReturnMonster(this);
    }



    public override void animUpdate()
    {

        anim.SetBool("isContact", Stop);
    }

    public virtual void ContactCheck() // 맞닿음 감지
    {
        RaycastHit2D rayhit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.02f), Vector2.left, 0.5f, LayerMask.GetMask("Player"));
        if (rayhit.collider != null)
        {

            Stop = true;

        }
        else
        {
            Stop = false;
        }
    }

    public virtual void TryAttack()
    {
        if (CurrentAttackDelay >= ApplyAttackDelay && !BattleManager.Instance.isDash)
        {
            anim.SetTrigger("Attack");
            CurrentAttackDelay = 0;
        }
    }

    public virtual void Attack()
    {
        BattleManager.Instance.PlayerDamage(Power);
    }

    public virtual void getHealed()
    {
        if (effect_Pos != null)
        {
            var obj = Objectpool.GetHealobject(transform.position);
            obj.transform.parent = effect_Pos;
        }
    }

    private void OnDisable()
    {
        setMonsterStat();
        applyPower = Power;
        applyMaxHp = MaxHp;
    }


    public void setMonsterStat()
    {
        if (BattleManager.Instance.loopCount != 0)
        {
                MaxHp = MaxHp * (2f * BattleManager.Instance.loopCount);
                Power = Power * (2f * BattleManager.Instance.loopCount);
                AddCoin = AddCoin + 1 * BattleManager.Instance.loopCount;
        }
    }
}



    


