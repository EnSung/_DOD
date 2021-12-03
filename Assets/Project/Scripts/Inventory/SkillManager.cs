using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DOD.DB;
public class SkillManager : Singleton<SkillManager>
{

    public List<int> Command;
    public int SKey;
    public string Name;
    public float Mana;
    public float Dmg;
    public float Ctime;
    public string Description;
    public int SkillLevel;
    public int UpgradeCost;
    public Image SkillIcon;

    void Awake()
    {
        base.Awake();
        
    }

    public void InitData()
    {
        Command = new List<int>();
        SKey = 0;
        Name = "";
        Mana = 0f;
        Dmg = 0f;
        Ctime = 0f;
        Description = "";
        SkillLevel = 0;
        UpgradeCost = 0;
    }
}
