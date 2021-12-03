using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DOD.DB;
public class SkillCoolTimeObject : MonoBehaviour
{
    public Image image;
    public Image filledPanel;
    public Text text;
    public RectTransform RT;
    public int skillKey;
    public string skillName;
    public float CoolTime;
    public float CurrentTime;
    bool isStop = true;
    void Awake()
    {
        skillKey = 10001;
        CoolTime = 10;
        CurrentTime = 10;
        RT = GetComponent<RectTransform>();


        RT.sizeDelta = new Vector2(RT.sizeDelta.x * ( (Screen.height * Camera.main.rect.height) / (float)1280), RT.sizeDelta.y * ( (Screen.height * Camera.main.rect.height)/(float)1280));
        
    }

    void OnEnable()
    {
        skillName = DataBaseManager.Instance.tdSkillDict[skillKey].Name;
        CoolTime = DataBaseManager.Instance.tdSkillDict[skillKey].T_Ctime;

        if (Resources.Load<Sprite>("SkillIcon/" + skillKey) != null)
        {
            image.sprite = Resources.Load<Sprite>("SkillIcon/" + skillKey) as Sprite;
        }

        CurrentTime = CoolTime;

        isStop = false;
    }
    void Update()
    {
        if (CurrentTime <= 0)
        {
            isStop = true;
            Objectpool.ReturnCTObj(this);
        }

        if (!isStop)
        {
            text.text = $"{skillName}\n{CurrentTime.ToString("N1")}";
            CurrentTime -= Time.deltaTime;
            filledPanel.fillAmount = CurrentTime / CoolTime;
        }
    }

    
}
