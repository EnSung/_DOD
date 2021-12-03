using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DOD.Define;
public class BossGenerater : MonoBehaviour
{
    int cnt = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (BattleManager.Instance.Score >= BattleManager.Instance.purpose && cnt <= 0)
        {
            BattleManager.Instance.isBoss = true;
            switch (BattleManager.Instance.currentmap)
            {
                case BattleManager.Stage.forest:
                   Objectpool.GetBossMonsterobject((int)bossEnum.거대나무, transform.position);

                    break;
                case BattleManager.Stage.GoblinViliage:
                    Objectpool.GetBossMonsterobject((int)bossEnum.고블린챔피언, transform.position);

                    break;
                default:
                    break;
            }
            cnt++;
        }
        if (!BattleManager.Instance.isBoss)
        {
            cnt = 0;
        }
    }
}
