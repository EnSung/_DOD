using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DOD.Define;

public class shamanGoblin : ProjectileMonster
{


    public Vector2 healArea;

    public override void Awake()
    {
        base.Awake();
        SetData((int)monsterEnum.샤먼고블린);
    }
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        TryAttack();
    }

    public override void TryAttack() // 힐체크
    {

        if (CurrentAttackDelay >= ApplyAttackDelay && !BattleManager.Instance.isDash)
        {
            anim.SetTrigger("Attack"); // 힐 (어택 X)
            CurrentAttackDelay = 0;
        }
    }

    public void shamanHeal()
    {

        Collider2D[] healMonster = Physics2D.OverlapBoxAll(transform.position, healArea, LayerMask.GetMask("Monster"));


        if (healMonster != null)
        {
            foreach (Collider2D monster in healMonster)
            {
                if (healMonster != null)
                {
                    if (monster != null)
                    {
                        try
                        {
                            monster.GetComponent<Monster>().Hp = monster.GetComponent<Monster>().Hp + applyPower;
                            monster.GetComponent<Monster>().getHealed();
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, healArea);
    }
}
