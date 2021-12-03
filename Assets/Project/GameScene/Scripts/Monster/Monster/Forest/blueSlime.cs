using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DOD.Define;

public class blueSlime : MeleeMonster
{
    public override void Awake()
    {
        base.Awake();
        SetData((int)monsterEnum.하늘슬라임);
    }
}
