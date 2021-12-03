using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DOD.Define;
public class GoblinViliageBoss : BossMonster
{

    [SerializeField]
    GameObject spawnCircle;


    float currentSKillDelay;
    public override void Awake()
    {
        base.Awake();
        SetData((int)bossEnum.고블린챔피언);

    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()   
    {
        base.Update();
        CurrentAttackDelay += Time.deltaTime;
        currentSKillDelay += Time.deltaTime;
        if(CurrentAttackDelay >= ApplyAttackDelay)
        {
            anim.SetTrigger("Attack");
        }
        passive_ATK_increase();
        if (currentSKillDelay >= SkillDelay)
        {

            int skillIndex = Random.Range(0, 2);
            anim.SetInteger("SkillIndex", skillIndex);
            switch (skillIndex)
            {
                case 0:
                    Skill_Smash();
                    break;
                case 1:
                    Skill_spawnGoblin();
                    break;

                default:
                    break;
            }

            currentSKillDelay = 0;
        }
    }


    public override void OnEnable()
    {
        base.OnEnable();

        applyPower = Power;
        currentSKillDelay = 0;

        applyMaxHp = MaxHp;
        Hp = applyMaxHp;
    }
    public void passive_ATK_increase()
    {
        float per = 100 - ((float)Hp / MaxHp) *100;
        applyPower = Power + per / 2;
    }

    public override void Attack()
    {
        BattleManager.Instance.PlayerDamage(applyPower);
    }

    public void Skill_spawnGoblin()
    {
        anim.SetInteger("SkillIndex", 1);
        anim.SetTrigger("Skill");
       
        
    }

    public void spawnGoblin()
    {
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, 3);

            switch (index)
            {
                case 0:
                    Instantiate(spawnCircle, new Vector2(transform.position.x + Random.Range(1, 3), 0.51f), Quaternion.identity);
                    break;
                case 1:
                    Instantiate(spawnCircle, new Vector2(transform.position.x + Random.Range(1, 3), 0.35f), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(spawnCircle, new Vector2(transform.position.x + Random.Range(1, 3), 0.123f), Quaternion.identity);
                    break;
                default:
                    break;
            }
        }
    }

    public void Skill_Smash() {
        anim.SetInteger("SkillIndex",0);
        anim.SetTrigger("Skill");
    }
}
