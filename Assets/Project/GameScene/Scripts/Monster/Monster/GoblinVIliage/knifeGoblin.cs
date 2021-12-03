using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DOD.Define;

public class knifeGoblin : MeleeMonster
{


    public override void Awake()
    {
        base.Awake();
        SetData((int)monsterEnum.단검고블린);
    }
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
}
