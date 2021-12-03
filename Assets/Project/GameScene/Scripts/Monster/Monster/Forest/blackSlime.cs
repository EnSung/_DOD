using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DOD.Define;

public class blackSlime : ProjectileMonster
{
    public override void Awake()
    {
        base.Awake();
        SetData((int)monsterEnum.검정슬라임);
    }
}
