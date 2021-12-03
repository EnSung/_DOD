using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
public class TDCharacter : TableBase
{
    public int UnitNo;
    public string Name;
    public float HP;
    public float THP;
    public float MP;
    public float TMP;
    public float POWER;
    public float TPOWER;
    public float Lhp;
    public float Lmp;
    public float Lpower;
    public float attackDistance;
    public float attakDelay;
    public int UpgradeCost;
    public int TUC;
    public int LUC;
    public override void SetJsonData(string key, JObject info)
    {
        base.SetJsonData(key, info);
        this.UnitNo = Int32.Parse(key);
        this.Name = info["Name"].Value<string>();
        this.HP = info["Hp"].Value<float>();
        this.MP = info["Mp"].Value<float>();
        this.POWER = info["Power"].Value<float>();
        this.Lhp = info["LHp"].Value<float>();
        this.Lmp = info["LMp"].Value<float>();
        this.Lpower = info["Lpower"].Value<float>();
        this.attackDistance = info["AttackDistance"].Value<float>();
        Debug.Log("1");
        this.attakDelay = info["AttackDelay"].Value<float>();
        Debug.Log("2");
        this.UpgradeCost = info["UpgrateCost"].Value<int>();
        this.LUC = info["LUC"].Value<int>();
    }
}
