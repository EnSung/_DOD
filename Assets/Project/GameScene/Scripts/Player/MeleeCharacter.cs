using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCharacter : Character
{
    
    public Transform AttackTransform; //공격 위치
    public float AttackRange; // 공걱 범위


    int FirstTargetcount = 0; // 첫번째 공격상대인지 판별하기위한 변수
    Collider2D target;// 공격타겟

    [HideInInspector]
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
    }

    public void SingleATK() // 단일공격 판정
    {
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(AttackTransform.position, AttackRange, LayerMask.GetMask("Monster")); // 콜라이더 받아오기

        foreach (Collider2D enemy in hitEnemy)
        {


            if (FirstTargetcount == 0) // 처음 공격이라면
            {

                target = enemy; // enemy를 타겟으로설정
            }

            FirstTargetcount++; // 카운트를 +1 해줘서 처음공격이아닌 두번째 공격이됨

            if (target.gameObject.activeSelf == false || target.gameObject == null) // 만약 타겟으로하던 게임오브젝트가 사라지거나 비활성화되면 = 죽으면
            {
                FirstTargetcount = 0; // 카운트를 0으로만들어 다음공격이 첫공격이됨
                target = enemy;
            }
    

            target.GetComponent<Monster>().Damaged(Power); // 타겟에게 데미지를줌
            break;
        }
    }
    public void MultiATK() // 다중공격 판정
    {
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(AttackTransform.position, AttackRange, LayerMask.GetMask("Monster")); // 콜라이더를 받아와서

        foreach (Collider2D enemy in hitEnemy)
        {

            enemy.GetComponent<Monster>().Damaged(ATKDamage); // 모든적에게 공격력만큼 데미지를줌
        }
    }

    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttackTransform.position, AttackRange);

    }

}
