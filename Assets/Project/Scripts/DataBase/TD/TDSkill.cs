using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class TDSkill : TableBase
{
    public int SKey;
    public string Name;
    public List<int> Command;
    public float Tmana; // totalMana
    public int Fmana;
    public float Lmana;
    public float Tdmg;
    public int Fdmg;
    public float Ldmg;
    public float T_Ctime;
    public int Ctime;
    public float L_Ctime;
    public float M_Ctime;
    public string Description;
    public int UpgradeCost;
    public int TUC;
    public int LCU;
    public int UnlockCost;
    public override void SetJsonData(string key, JObject info)
    {
        base.SetJsonData(key, info);
        
        SKey = Int32.Parse(key);
        Name = info["Name"].Value<string>();
        Command = new List<int>(Array.ConvertAll(info["Command"].Value<string>().Split(','), int.Parse));
        Fmana = info["Fmana"].Value<int>();
        Lmana = info["Lmana"].Value<float>();
        Fdmg = info["Fdmg"].Value<int>();
        Ldmg = info["Ldmg"].Value<float>();
        Ctime = info["Ctime"].Value<int>();
        L_Ctime = info["L_Ctime"].Value<float>();
        M_Ctime = info["M_Ctime"].Value<float>();
        Description = info["Description"].Value<string>();
        UpgradeCost = info["UpgrateCost"].Value<int>();
        LCU = info["LCU"].Value<int>();
        UnlockCost = info["UnlockCost"].Value<int>();
    }

}
