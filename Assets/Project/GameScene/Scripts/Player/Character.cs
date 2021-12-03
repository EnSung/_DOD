    using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DOD.DB;
using DOD.Define;

public class Character : Actor
{
    /* public string characterName;
     public float MaxHp;
     [HideInInspector]
     public float Hp;

     
     [Range(1f,5f)]
     public float AttackDistance; // 공격 거리
     public float AttackDelay;
     public float StrikingPower;


     Animator anim;*/
    public float MaxMp; // 최대 MP
    [HideInInspector]
    public float Mp; // 현재 MP
    
    public bool isAttack;

    public Slider hpSlider;
    public Slider mpSlider;
    public TextMeshProUGUI HpText;

    public float ATKDamage;

    public float decreaseCooltimeSpeed;

    public bool isParing; // 패링중인가 (흘리기 시전중인가? or 무적상태인가)

    public override void Start()
    {
        base.Start();
        Hp = MaxHp;
        Mp = MaxMp;
        StartCoroutine(HpMpRecovery(1, 1, 2));
    }

public override void Update()
    {
        
        base.Update();
        animUpdate();
        Attack();
        DieCheck();
        hp_mp_UIUpdate();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        AttackStateCheck();

    }

    public override void SetData(int Key)
    {
        this.unitNo = DataBaseManager.Instance.tdCharacterDict[Key].UnitNo;
        this.Name = DataBaseManager.Instance.tdCharacterDict[Key].Name;
        this.MaxHp = DataBaseManager.Instance.tdCharacterDict[Key].THP;
        this.AttackDistance = DataBaseManager.Instance.tdCharacterDict[Key].attackDistance;
        this.AttackDelay = DataBaseManager.Instance.tdCharacterDict[Key].attakDelay;
        this.Power = DataBaseManager.Instance.tdCharacterDict[Key].TPOWER;
        this.MaxMp = DataBaseManager.Instance.tdCharacterDict[Key].TMP;
    }
    public override void Damaged(float value) // 데미지 입는함수
    {
        if (Hp > 0)
        {
            if (!BattleManager.Instance.isUseSkill)
            {
                anim.SetTrigger("Damaged");
            }
            Hp -= value;
        }
    }

    /// <summary>
    /// MP가 충분한지 체크하는 함수
    /// </summary>
    /// <param name="value">체크하는 MP값</param>
    /// <returns></returns>
    public bool UseMpCheck(float value) 
    {
        if (Mp - value >= 0)
        {
            
            return true;
        }
        else
        {
            GameSceneUIManager.Instance.ApearWarningText(1);
            return false;
        }

    }

    /// <summary>
    /// MP를 사용하는 함수
    /// </summary>
    /// <param name="value">사용하는 MP값</param>
    public void UseMp(float value)
    {
        Mp -= value;
    }


    /// <summary>
    /// 죽음 체크
    /// </summary>
    public override void DieCheck() 
    {
        if (Hp <= 0)
        {
            BattleManager.Instance.isEnd = true;
            anim.SetBool("isDie", true);
            return;
        }
    }


    /// <summary>
    /// 스탯 재생 코루틴
    /// </summary>
    /// <param name="second"> 몇 초마다</param>
    /// <param name="Hpvalue">HP를 얼마나</param>
    /// <param name="Mpvalue">MP를 멀마나</param>
    /// <returns></returns>
    public virtual IEnumerator HpMpRecovery(float second, float Hpvalue, float Mpvalue)
    {
        while (!BattleManager.Instance.isEnd)
        {
            yield return new WaitForSeconds(second);
            if (Hp < MaxHp)
            {
                Hp += Hpvalue;
            }
            if (Mp < MaxMp)
            {
                Mp += Mpvalue;
            }
        }
    }
    /// <summary>
    /// 적과 맞닿아있는지 체크후 애니메이터에 적용
    /// </summary>
    public override void animUpdate()
    {
        anim.SetBool("isContact", BattleManager.Instance.isContact);
    }


    /// <summary>
    /// UseSkill를 false로 설정하는 함수 (애니메이션 이벤트로 작동)
    /// </summary>
    public virtual void SetisUseSkillFalse()
    {
        BattleManager.Instance.isUseSkill = false;
    }

    /// <summary>
    /// 무적 상태 비활성화(애니메이션 이벤트로 작동)
    /// </summary>
    public virtual void SetisParingFalse()
    {
        isParing = false;
    }

    /// <summary>
    /// 공격가능 상태 판별
    /// </summary>
    public virtual void AttackStateCheck() 
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.right, AttackDistance, LayerMask.GetMask("Monster"));

        isAttack = (hitinfo.collider != null && !BattleManager.Instance.isUseSkill) ? true : false;
        BattleManager.Instance.isContact = (hitinfo.collider != null &&!BattleManager.Instance.isUseSkill) ? true : false;

    }

    /// <summary>
    /// HP MP UI 적용
    /// </summary>
    public virtual void hp_mp_UIUpdate()
    {
        hpSlider.value = Mathf.Lerp(hpSlider.value, Hp/MaxHp, 8f * Time.deltaTime);
        mpSlider.value = Mathf.Lerp(mpSlider.value, Mp / MaxMp, 8f * Time.deltaTime);
        HpText.text = Hp.ToString("F0") + " / " + MaxHp.ToString("F0");
    }

    /// <summary>
    /// 공격
    /// </summary>
    public virtual void Attack()
    {

        if (isAttack)
        {


            if (!BattleManager.Instance.isUseSkill)
            {

                if (CurrentAttackDelay >= ApplyAttackDelay && !BattleManager.Instance.isDash)
                {
                    anim.SetTrigger("Attack");

                    CurrentAttackDelay = 0;
                }
            }
        }
    }

    /// <summary>
    /// 스킬 사용 함수
    /// </summary>
    /// <param name="SE">스킬종류</param>
    /// <param name="list">현재 입력한 커맨드</param>
    /// <param name="CoolTimeCheck"> 현재 쿨타임</param>
    /// <returns></returns>
    public bool UseSkill(skillEnum SE, List<int> list, float CoolTimeCheck)
    {



        if (BattleManager.Instance.PatternInputEnd)
        {

            if (!BattleManager.Instance.isUseSkill)
            {
                
                if (UserDataManager.user.skill_level[DataBaseManager.Instance.tdSkillDict[(int)SE].SKey.ToString()] != 0)
                {

                    if (UseMpCheck(DataBaseManager.Instance.tdSkillDict[(int)SE].Tmana))
                    {


                        if (BattleManager.Instance.Pattern_id.SequenceEqual(DataBaseManager.Instance.tdSkillDict[(int)SE].Command))
                        {
                            if (CoolTimeCheck <= 0)
                            {
                                GameSceneUIManager.Instance.Create_SkillCoolTimeObject((int)SE);
                                return true;
                            }
                            else
                            {
                                GameSceneUIManager.Instance.ApearWarningText(2);
                            }
                        }
                    }

                }
                else
                {
                    //GameSceneUIManager.Instance.ApearWarningText(3);
                }

            }

        }

        return false;

    }


}
