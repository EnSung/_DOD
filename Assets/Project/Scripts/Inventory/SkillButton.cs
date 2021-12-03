using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DOD.DB;
public class SkillButton : MonoBehaviour
{
    [SerializeField]
    public bool isOpenSkill = false;
    [SerializeField]
    public int SKey;
    [SerializeField]
    public string Name;
    [SerializeField]
    public List<int> Command;
    [SerializeField]
    public float Mana;
    [SerializeField]
    public float Dmg;
    [SerializeField]
    public string Description;
    [SerializeField]
    public float Ctime;
    [SerializeField]
    public int SkillLevel;
    [SerializeField]
    public int UpgradeCost;

    public GameObject Skill;

    public void SetButton(int skey)
    {
        this.SKey = skey;
        var skillDict = DataBaseManager.Instance.tdSkillDict[this.SKey];
        //string nameKey = skillDict.Name; ?? 

        this.Name = skillDict.Name;
        this.Command = skillDict.Command;
        this.Description = skillDict.Description;
        this.Mana = skillDict.Tmana;
        this.Dmg = skillDict.Tdmg;
        this.SkillLevel = UserDataManager.user.skill_level[skillDict.SKey.ToString()];
        this.Ctime = skillDict.T_Ctime;
        if (this.SkillLevel < 1 && !isOpenSkill)
        {
            this.UpgradeCost = skillDict.UnlockCost;
        }
        else
        {
            this.UpgradeCost = skillDict.TUC;
        }
        //Debug.Log("name : " + nameKey);
    }

    public void OnClickSkillButton()
    {
        LobbyUIManager.Instance.OpenSkillPanel(Skill);
    }

    public void Upgrade()
    {
        var coin = UserDataManager.user.coin;
        if (coin < UpgradeCost)
        {
            StartCoroutine(LobbyUIManager.Instance.NoMoney());
            return;
        }
        if (!isOpenSkill)
        {
            isOpenSkill = true;
        }
        UserDataManager.user.coin -= UpgradeCost;
        SkillLevel += 1;
        UserDataManager.user.skill_level[SKey.ToString()] += 1;
        SoundManager.Instance.SFXPlay("레벨 업", LobbyUIManager.Instance.Levelup);
    }
}
