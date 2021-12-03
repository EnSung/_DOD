using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public float offset;
    public float speed; // 배경 속도
    public Material[] mats; // 배경 머테리얼
    Animator anim; // 배경 애니메이터
    private void Start()
    {
        speed = 0.35f;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (BattleManager.Instance.isEnd) // 게임이 끝이면 멈추기
        {
            return;
        }

        if (!BattleManager.Instance.isContact && !BattleManager.Instance.isStop && !BattleManager.Instance.isUseSkill) // 플레이어가 몬스터와 싸우고있을땐 멈추기
        {
            switch (BattleManager.Instance.currentmap)
            {
                case BattleManager.Stage.forest:
                    anim.SetInteger("index", 1);
                    
                    break;
                case BattleManager.Stage.GoblinViliage:
                    anim.SetInteger("index", 2);

                    break;
                default:
                    break;
            }

            foreach (Material i in mats)
            {
                i.mainTextureOffset = new Vector2(offset, 0);

            }
            if (offset >= 100)
            {
                offset = 0;
            }
            offset += Time.deltaTime * speed;

            
        }
    }

    
}
