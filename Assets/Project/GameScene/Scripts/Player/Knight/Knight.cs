    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DOD.DB;
using DOD.Define;

public class Knight : MeleeCharacter
{


    public Transform originPos; //원래 위치(돌진)
    public Transform DashTransform; // 돌진으로 가야하는 위치(돌진)
    public Transform BigSword_ATK_Transform; // 내려찍기 판정 위치
    public Transform Paring_ATK_Transform; // 흘리기 판정 위치
    Dictionary<string, float> currentSkillcoolTimeDic = new Dictionary<string, float>();
    Dictionary<string, float> SkillcoolTimeDic = new Dictionary<string, float>();


    DataBaseManager DB = DataBaseManager.Instance;


    public GameObject SwordAura_Projectile;
    public override void Awake()
    {
        SetData((int)characterEnum.기사);

    }
    public override void Start()
    {

        base.Start();

        foreach (var item in DataBaseManager.Instance.tdSkillDict.Values)
        {
            SkillcoolTimeDic.Add(item.Name, item.T_Ctime);
            currentSkillcoolTimeDic.Add(item.Name, 0);
        }

    }
    public override void Update()
    {
        base.Update();
        Skill_Dash();
        Skill_Sting();
        Skill_SpinAttack();
        Skill_BigSword();
        Skill_SwordAura();
        Skill_ThreeCut();
        Skill_Healing();
        Skill_paring();
        Skill_push();
    }

    #region 판정
    public void BigSwordATK() // 내려찍기 판정
    {
        Collider2D[] hitEnemy = Physics2D.OverlapBoxAll(BigSword_ATK_Transform.position, new Vector2(4,1), 0, LayerMask.GetMask("Monster"));

        foreach (Collider2D enemy in hitEnemy)
        {
            enemy.GetComponent<Monster>().Damaged(ATKDamage);
        }
    }


    public void ParingATK() // 흘리기 판정
    {
        Collider2D[] hitEnemy = Physics2D.OverlapBoxAll(Paring_ATK_Transform.position, new Vector2(2.3f,1), 0, LayerMask.GetMask("Monster"));

        foreach (Collider2D enemy in hitEnemy)
        {
            enemy.GetComponent<Monster>().Damaged(ATKDamage);
        }
    }

    #endregion

    #region 스킬

    #region 돌진

    /// <summary>
    /// 돌진
    /// </summary>
    public void Skill_Dash()
    {

        currentSkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.돌진].Name] -= Time.deltaTime;

        if (UseSkill(skillEnum.돌진, BattleManager.Instance.Pattern_id, currentSkillcoolTimeDic[DataBaseManager.Instance.tdSkillDict[(int)skillEnum.돌진].Name]))
        {
            ATKDamage = DB.tdSkillDict[(int)skillEnum.돌진].Tdmg;
            Debug.Log(DB.tdSkillDict[(int)skillEnum.돌진].Tdmg);
            UseMp(DB.tdSkillDict[(int)skillEnum.돌진].Tmana);
            StartCoroutine(DashCroutine());
        }








    }
    /// <summary>
    /// 돌진 판정
    /// </summary>
    IEnumerator DashCroutine()
    {
        currentSkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.돌진].Name] = SkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.돌진].Name];

        float currentTime = 1;
        BattleManager.Instance.isUseSkill = true;
        BattleManager.Instance.isDash = true;


        anim.SetBool("isDash", true);
        while (transform.position.x < DashTransform.position.x - 0.2f)
        {
            currentTime += Time.deltaTime;
            BattleManager.Instance.isUseSkill = true;
            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(AttackTransform.position, 0.2f, LayerMask.GetMask("Monster"));
            foreach (Collider2D enemy in hitEnemy)
            {
                enemy.transform.position = new Vector2(transform.position.x + 0.6f, enemy.transform.position.y);
                if (currentTime >= 0.4f)
                {
                    
                    enemy.GetComponent<Monster>().Damaged(ATKDamage);
                }

            }

            transform.position = Vector2.Lerp(transform.position, DashTransform.position, 2.2f * Time.deltaTime);
            if (currentTime >= 0.4f)
            {

                currentTime = 0;
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        anim.SetBool("isDash", false);

        while (transform.position.x > originPos.transform.position.x + 0.1f)
        {

            transform.position = Vector2.Lerp(transform.position, originPos.transform.position, 6 * Time.deltaTime);
            yield return null;
        }


        transform.position = originPos.position;


        yield return new WaitForSeconds(0.1f);
        BattleManager.Instance.isUseSkill = false;
        BattleManager.Instance.isDash = false;




    }

    #endregion

    #region 연속찌르기

    /// <summary>
    /// 연속 찌르기
    /// </summary>
    public void Skill_Sting()
    {
        currentSkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.연속찌르기].Name] -= Time.deltaTime;

        if (UseSkill(skillEnum.연속찌르기, BattleManager.Instance.Pattern_id,currentSkillcoolTimeDic[DataBaseManager.Instance.tdSkillDict[(int)skillEnum.연속찌르기].Name]))
        {
            ATKDamage = DB.tdSkillDict[(int)skillEnum.연속찌르기].Tdmg;
            BattleManager.Instance.isUseSkill = true;
            UseMp(DB.tdSkillDict[(int)skillEnum.연속찌르기].Tmana);
            anim.SetTrigger("Sting");
            currentSkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.연속찌르기].Name] = SkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.연속찌르기].Name];
        }



    }
    #endregion

    #region 회전공격
    public void Skill_SpinAttack()
    {
        currentSkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.회전공격].Name] -= Time.deltaTime;
        if(UseSkill(skillEnum.회전공격, BattleManager.Instance.Pattern_id,currentSkillcoolTimeDic[DataBaseManager.Instance.tdSkillDict[(int)skillEnum.회전공격].Name]))
        {
            ATKDamage = DB.tdSkillDict[(int)skillEnum.회전공격].Tdmg;
            UseMp(DB.tdSkillDict[(int)skillEnum.회전공격].Tmana);
            StartCoroutine(SpinAttack());

        }
    }


    IEnumerator SpinAttack()
    {
        currentSkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.회전공격].Name] =
            SkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.회전공격].Name];

        BattleManager.Instance.isUseSkill = true;
        anim.SetBool("isSpinAttack", true);

        yield return new WaitForSeconds(3f);

        anim.SetBool("isSpinAttack", false);

        yield return new WaitForSeconds(0.7f);

        BattleManager.Instance.isUseSkill = false;


    }
    #endregion

    #region 내려찍기

    public void Skill_BigSword()
    {
        currentSkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.내려찍기].Name] -= Time.deltaTime;
        

        if (UseSkill(skillEnum.내려찍기,BattleManager.Instance.Pattern_id, currentSkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.내려찍기].Name]))
        {
            ATKDamage = DataBaseManager.Instance.tdSkillDict[(int)skillEnum.내려찍기].Tdmg;
            BattleManager.Instance.isUseSkill = true;
            anim.SetTrigger("BigSword");
            UseMp(40);

            currentSkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.내려찍기].Name] =
            SkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.내려찍기].Name];
        }
    }




    #endregion

    #region 검기 날리기


    /// <summary>
    /// 스킬 체크
    /// </summary>
    public void Skill_SwordAura()
    {


        currentSkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.검기날리기].Name] -= Time.deltaTime;

        if (UseSkill(skillEnum.검기날리기, BattleManager.Instance.Pattern_id, currentSkillcoolTimeDic[DataBaseManager.Instance.tdSkillDict[(int)skillEnum.검기날리기].Name]))
        {

            BattleManager.Instance.isUseSkill = true;
            ATKDamage = DB.tdSkillDict[(int)skillEnum.검기날리기].Tdmg;
            UseMp(DB.tdSkillDict[(int)skillEnum.검기날리기].Tmana);

            anim.SetTrigger("SwordAura");

            currentSkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.검기날리기].Name] = SkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.검기날리기].Name];
        }

    }

    public void Create_SwordAura_Projectile()
    {
        var obj = Instantiate(SwordAura_Projectile, new Vector2(transform.position.x + 0.2f, transform.position.y + 0.7f),Quaternion.identity);
        obj.GetComponent<Knight_SwordAura_Projectile>().Init(ATKDamage, 6);
    }




    #endregion

    #region z자베기

    public void Skill_ThreeCut()
    {
        currentSkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.z자베기].Name] -= Time.deltaTime;

        if (UseSkill(skillEnum.z자베기, BattleManager.Instance.Pattern_id, currentSkillcoolTimeDic[DataBaseManager.Instance.tdSkillDict[(int)skillEnum.z자베기].Name]))
        {
            ATKDamage = DB.tdSkillDict[(int)skillEnum.z자베기].Tdmg;
            Debug.Log(DB.tdSkillDict[(int)skillEnum.z자베기].Tdmg);
            BattleManager.Instance.isUseSkill = true;
            UseMp(DB.tdSkillDict[(int)skillEnum.z자베기].Tmana);
            anim.SetTrigger("ThreeCut");
            currentSkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.z자베기].Name] = SkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.z자베기].Name];
        }
    }
    #endregion

    #region 회복
    public void Skill_Healing()
    {
        currentSkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.회복].Name] -= Time.deltaTime;

        if (UseSkill(skillEnum.회복, BattleManager.Instance.Pattern_id, currentSkillcoolTimeDic[DataBaseManager.Instance.tdSkillDict[(int)skillEnum.회복].Name]))
        {
            BattleManager.Instance.isUseSkill = true;
            UseMp(DB.tdSkillDict[(int)skillEnum.회복].Tmana);
            anim.SetTrigger("Healing");
            StartCoroutine(HealingCroutine(8, 1, DB.tdSkillDict[(int)skillEnum.회복].Tdmg));
            currentSkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.회복].Name] = SkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.회복].Name];
        }
    }

    IEnumerator HealingCroutine(float duration,float interval ,float HPMP)
    {
        float durationTime = Time.time + duration;
        float current = 0;

        while(Time.time <= durationTime)
        {
            yield return null;
            current += Time.deltaTime;
            if(current >= interval)
            {
                Hp += HPMP;
                Mp += HPMP;
                current = 0;
            }
        }
    }
    #endregion

    #region 흘리기

    public void Skill_paring()
    {

        currentSkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.흘리기].Name] -= Time.deltaTime;

        if (UseSkill(skillEnum.흘리기, BattleManager.Instance.Pattern_id, currentSkillcoolTimeDic[DataBaseManager.Instance.tdSkillDict[(int)skillEnum.흘리기].Name]))
        {
            BattleManager.Instance.isUseSkill = true;
            isParing = true;
            ATKDamage = DB.tdSkillDict[(int)skillEnum.흘리기].Tdmg;
            BattleManager.Instance.isUseSkill = true;
            UseMp(DB.tdSkillDict[(int)skillEnum.흘리기].Tmana);
            anim.SetTrigger("Paring");
            currentSkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.흘리기].Name] = SkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.흘리기].Name];
        }
    }
    #endregion

    #region 밀쳐내기

    public void Skill_push()
    {

        currentSkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.밀쳐내기].Name] -= Time.deltaTime;

        if (UseSkill(skillEnum.밀쳐내기, BattleManager.Instance.Pattern_id, currentSkillcoolTimeDic[DataBaseManager.Instance.tdSkillDict[(int)skillEnum.밀쳐내기].Name]))
        {
            BattleManager.Instance.isUseSkill = true;
            ATKDamage = DB.tdSkillDict[(int)skillEnum.밀쳐내기].Tdmg;
            UseMp(DB.tdSkillDict[(int)skillEnum.밀쳐내기].Tmana);
            anim.SetTrigger("Push");
            currentSkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.밀쳐내기].Name] = SkillcoolTimeDic[DB.tdSkillDict[(int)skillEnum.밀쳐내기].Name];
        }
    }

    public void Push()
    {
            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(AttackTransform.position, AttackRange, LayerMask.GetMask("Monster")); // 콜라이더를 받아와서

            foreach (Collider2D enemy in hitEnemy)
            {

                enemy.GetComponent<Monster>().Damaged(ATKDamage); // 모든적에게 공격력만큼 데미지를줌
            if (enemy.tag == "Monster")
            {

                enemy.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(enemy.gameObject.GetComponent<Rigidbody2D>().velocity.x + 15, enemy.gameObject.GetComponent<Rigidbody2D>().velocity.y);
                enemy.gameObject.GetComponent<Monster>().Damaged(ATKDamage);

            }
            else if (enemy.tag == "immuneMonster")
            {
                enemy.gameObject.GetComponent<Monster>().Damaged(ATKDamage);

            }
        }
        
    }
    #endregion
    #endregion


    public override void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(BigSword_ATK_Transform.position,new Vector2(4,1));
        Gizmos.DrawWireCube(Paring_ATK_Transform.position,new Vector2(2.3f,1));

    }

}
