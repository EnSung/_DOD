using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DOD.Define;
using DOD.DB;

public class BossMonster : Monster
{
    public GameObject BossState;
    public Slider bossHpbar;
    public Text bossName;
    public Text hpText;

    public GameObject[] BossUIPos;
    public bool isApear;
    public float SkillDelay;


    public int cnt; // 맞은 횟수 
    public override void Awake()
    {
        base.Awake();

        BossState = GameObject.FindGameObjectWithTag("BossUI");
        bossHpbar = BossState.GetComponentInChildren<Slider>();
        bossName = GameObject.FindGameObjectWithTag("bossName").GetComponent<Text>();
        hpText = GameObject.FindGameObjectWithTag("bossHp").GetComponent<Text>();
        BossUIPos = GameObject.FindGameObjectsWithTag("BossUIPos");

    }

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        applySpeed = Speed;
        ApplyAttackDelay = AttackDelay;

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

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        ContactCheck();
    }


    public override void DieCheck()
    {
        base.DieCheck();
    }


    public override void OnEnable()
    {
        base.OnEnable();
        Hp = applyMaxHp *(BattleManager.Instance.loopCount +1);
        applyPower = Power * (BattleManager.Instance.loopCount +1);
        bossName.text = Name.ToString();

        MaxHp *= (BattleManager.Instance.loopCount + 1);
        applyMaxHp = MaxHp * (BattleManager.Instance.loopCount +1);
        Hp = applyMaxHp * (BattleManager.Instance.loopCount +1);
        
        BossState.transform.position = BossUIPos[0].transform.position;

        bossHpbar.value = MaxHp;
        StartCoroutine(appear());

    }
    public override void Die()
    {
        if (!BattleManager.Instance.isEnd)
        {
            if (BossUIPos[1] != null)
            {
                BossState.transform.position = BossUIPos[1].transform.position;
            }
            BattleManager.Instance.isBoss = false;
            BattleManager.Instance.purpose += BattleManager.Instance.BossInterval;
            BattleManager.Instance.startPanel.GetComponent<Animator>().SetTrigger("isStart");
            BattleManager.Instance.nextStage();
            BattleManager.Instance.SetmonsterGenInterval(5f);

        }
        Objectpool.ReturnBossMonster(this);
    }

    public void OnDisable()
    {
        try
        {
            BossState.transform.position = BossUIPos[1].transform.position;
        }
        catch
        {

        }
            setMonsterStat();
        applyPower = Power;
        applyMaxHp = MaxHp;
    }

    public override void Damaged(float value)
    {
        Objectpool.GetDamageText(canvas, DamageText_SpawnPoint.position, ((int)value).ToString());
        if (cnt >= 5)
        {
            Hp -= value;
            anim.SetTrigger("Damaged");
            cnt = 0;
        }
        else
        {
            Hp -= value;

        }
        cnt++;

    }
    
    /// <summary>
    /// 체력바 업데이트
    /// </summary>
    public void hpUpdate()
    {
        hpText.text = $"{(int)Hp} / {(int)MaxHp}";
        if (Hp < 0)
        {
            Hp = 0;
        }
    }

    public override void animUpdate()
    {
        base.animUpdate();
    }

    public override void SetData(int Key)
    {
        var dict = DataBaseManager.Instance.tdBossDict[Key];
        this.unitNo = dict.UnitNo;
        this.Name = dict.Name;
        this.MaxHp = dict.HP;
        this.applyMaxHp = dict.HP;
        this.Power = dict.Power;
        this.applyPower = dict.Power;
        this.Speed = dict.Speed;
        this.AttackDistance = dict.attackDistance;
        this.AttackDelay = dict.attakDelay;
        this.ApplyAttackDelay = dict.attakDelay;
        this.AddScore = dict.AddScore;
        this.AddCoin = dict.AddCoin;
        this.CoinAmount = dict.CoinAmount;
        this.SkillDelay = dict.SkillDelay;
    }

   


    public IEnumerator appear()
    {
        while (bossHpbar.value <= 0.99f)
        {
            yield return null;
            bossHpbar.value = Mathf.Lerp(bossHpbar.value, 1, 3 * Time.deltaTime);
        }
        isApear = true;

        StopCoroutine(appear());
    }

    public void SetHp(float hp, float maxhp)
    {
        bossHpbar.value = Mathf.Lerp(bossHpbar.value, hp / maxhp, 6 * Time.deltaTime);
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
        if (BattleManager.Instance.isEnd)
        {
            return;
        }
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