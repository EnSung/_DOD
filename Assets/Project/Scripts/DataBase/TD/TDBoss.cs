using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TDBoss : TableBase
{
    public int UnitNo;
    public string Name;
    public float HP;
    public float Power;
    public float Speed;
    public float attackDistance;
    public float attakDelay;
    public float AddScore;
    public int AddCoin;
    public int CoinAmount;
    public float SkillDelay;



    public override void SetJsonData(string key, JObject info)
    {
        base.SetJsonData(key, info);

        UnitNo = Int32.Parse(key);
        Name = info["Name"].Value<string>();
        HP = info["Hp"].Value<int>();
        Power = info["Power"].Value<float>();
        Speed = info["Speed"].Value<float>();
        attakDelay = info["AttackDelay"].Value<float>();
        attackDistance = info["AttackDistance"].Value<float>();
        AddScore = info["AddScore"].Value<float>();
        AddCoin = info["AddCoin"].Value<int>();
        CoinAmount = info["CoinAmount"].Value<int>();
        SkillDelay = info["SkillDelay"].Value<float>();
    }
}
