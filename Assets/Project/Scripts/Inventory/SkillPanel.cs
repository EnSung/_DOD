using DOD.DB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    public Text Name;
    public Image SkillIcon;
    public Text SkillLevel;
    public Text Description;

    public Text ATKText;
    public Text ManaText;
    public Text CTimeText;
    public Text CommandText;
    public Text DescriptionText;
    public Text UpgradeCostText; // 업그레이드 비용 계산 필요 (고정값 * 스킬 레벨)

    private void Update()
    {

    }

    public void LoadSkillData()
    {
        var i = SkillManager.Instance;
        string cmd = "";
        foreach (int j in i.Command)
        {
            if (j == i.Command[i.Command.Count - 1])
            {
                cmd += j + 1;
            }
            else
            {
                cmd += j + 1 + "-";
            }
        }
        Name.text = i.Name;
        if (i.SkillLevel < 1)
        {
            SkillIcon.sprite = Resources.Load<Sprite>("SkillIcon/" + "10000") as Sprite;
            ATKText.text = "해금 필요";
            ManaText.text = "해금 필요";
            CommandText.text = "해금 필요";
            CTimeText.text = "해금 필요";
            SkillLevel.text = "해금 필요";
            DescriptionText.text = "해금 필요";
            Name.text = i.Name;
            UpgradeCostText.text = i.UpgradeCost.ToString();
        }
        else
        {
            SkillIcon.sprite = Resources.Load<Sprite>("SkillIcon/" + i.SKey) as Sprite;
            SkillLevel.text = i.SkillLevel.ToString("LV : 0");
            Description.text = i.Description;
            ATKText.text = i.Dmg.ToString();
            ManaText.text = i.Mana.ToString();
            CommandText.text = cmd;
            CTimeText.text = i.Ctime.ToString("0초");

            DescriptionText.text = i.Description;
            UpgradeCostText.text = i.UpgradeCost.ToString();
        }
    }


}
