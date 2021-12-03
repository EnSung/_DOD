using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
public class TDMonster : TableBase
{
    public int unitNo;
    public string Name;
    public float Hp;
    public float Power;
    public float Speed;
    public float AttackDelay;
    public float AttackDistance;
    public float AddScore;
    public int AddCoin;
    public int CoinAmount;

    public override void SetJsonData(string key, JObject info)
    {
        base.SetJsonData(key, info);

        unitNo = Int32.Parse(key);
        Name = info["Name"].Value<string>();
        Hp = info["Hp"].Value<int>();
        Power = info["Power"].Value<float>();
        Speed = info["Speed"].Value<float>();
        AttackDelay = info["AttackDelay"].Value<float>();
        AttackDistance = info["AttackDistance"].Value<float>();
        AddScore = info["AddScore"].Value<float>();
        AddCoin = info["AddCoin"].Value<int>();
    }
}
