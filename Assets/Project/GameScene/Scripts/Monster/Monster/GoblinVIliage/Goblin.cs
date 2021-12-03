using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DOD.Define;

public class Goblin : MeleeMonster
{

    public override void Awake()
    {
        base.Awake();
        SetData((int)monsterEnum.고블린);

    }
    public override void Start()
    {
        base.Start();
    }



    public override void Update()
    {
        base.Update();
    }
}
