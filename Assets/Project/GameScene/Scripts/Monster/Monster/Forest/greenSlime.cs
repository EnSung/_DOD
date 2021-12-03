using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DOD.Define;

public class greenSlime : ProjectileMonster
{
    public override void Awake()
    {
        base.Awake();
        SetData((int)monsterEnum.초록슬라임);
    }
}
