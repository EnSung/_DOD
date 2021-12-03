using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DOD.Define;

public class giantGoblin : MeleeMonster
{
    public override void Awake()
    {
        base.Awake();
        SetData((int)monsterEnum.큰고블린);
    }
}
