using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DOD.Define;

public class purpleSlime : MeleeMonster
{
    public override void Awake()
    {
        base.Awake();
        SetData((int)monsterEnum.보라슬라임);
    }
}
