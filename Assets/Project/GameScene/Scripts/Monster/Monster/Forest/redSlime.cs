using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DOD.Define;
public class redSlime : MeleeMonster
{
    public override void Awake()
    {
        base.Awake();
        SetData((int)monsterEnum.빨강슬라임);
    }
}
