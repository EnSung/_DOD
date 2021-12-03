using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject SkillPanel;

    public void OnClickBackArrow()
    {
        
    }

    public void OnClickSkillIcon()
    {
        SkillPanel.SetActive(true);
        GameManager.Instance.StatSetting();

    }
}
