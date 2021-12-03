using DOD.DB;
using DOD.Define;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : Singleton<LobbyUIManager>
{
    #region 오브젝트
    public Image StartButton;
    public Sprite OnStartButton;
    public Sprite OffStartButton;

    public Text Coin;
    public Text InvenCoin;
    public Animator InventoryAnim;
    public GameObject InventoryIcon;

    public Animator Canvas;
    public GameObject Leave;
    public Animator Character;

    public GameObject SkillButtonObj;
    public Transform SkillObjParent;

    public GameObject WarningText;

    public GameObject SkillPanel;
    public GameObject Inventory;
    public GameObject rankPanel;
    public GameObject nameInput;
    public GameObject quitPanel;
    public GameObject InvenCharacter;

    public GameObject SkillStatPanel;
    public GameObject SkillDescPanel;
    public GameObject SkillStatButton;
    public GameObject SkillDescButton;
    
    public Text Characterlvl;
    public Text CharUpgradeCost;

    public GameObject CharUpgrade;

    private GameObject CurrentSelectedSkill;

    bool isQuitPanel = false;
    public int InvenCharKey;

    [Header("Rank")]
    public MultiMap<int,GameObject> rankObjQ = new MultiMap<int, GameObject>(); // 랭크 오브젝트 큐(오브젝트 풀)
    public GameObject rankObj_1;
    public GameObject rankObj_2;
    public GameObject rankObj_3;
    public GameObject rankObj_Default;

    public GameObject SoundPanel;
    public GameObject BG_Check;
    public GameObject SFX_Check;

    public GameObject TutorialPanel;
    private GameObject TutorialImage;
    private int TutorialNumber;
    #endregion

    #region 사운드
    public AudioClip ClickButton;
    public AudioClip Levelup;
    public AudioClip BgSounds;
    #endregion

    protected override void Awake()
    {
        InvenCharKey = 30001;
        TutorialImage = TutorialPanel.transform.GetChild(0).gameObject;
        TutorialNumber = 1;
        StartCoroutine(StartScene());
        if (PlayerPrefs.GetFloat("BGSound") == -80f)
        {
            BG_Check.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetFloat("SFX") == -80f)
        {
            SFX_Check.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    IEnumerator StartScene()
    {
        //InvenCharacter.GetComponent<InvenChar>().LoadData(InvenCharKey);
        CreateNewrankObj();
        
        Canvas.SetTrigger("LeaveOpen");
        yield return new WaitForSeconds(2.2f);
        SoundManager.Instance.BgSound(BgSounds);
        Leave.SetActive(false);
        Character.SetTrigger("IDLE");

        nickNameInput();

    }

    public IEnumerator NoMoney()
    {
        WarningText.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        WarningText.SetActive(false);
    }

    void nickNameInput()
    {
        if (!UserDataManager.Instance.isexist)
        {
            if (!GameManager.Instance.isnameInput)
            {
                Tutorial();
                nameInput.SetActive(true);
            }
        }

    }

    #region 오브젝트 풀
    private void CreateNewrankObj()
    {
        for (int i = 0; i < 2; i++)
        {
            var newObj1 = Instantiate(rankObj_1);
            newObj1.transform.parent = Instance.transform;
            newObj1.gameObject.SetActive(false);
            rankObjQ.Add(1, newObj1);

            var newObj2 = Instantiate(rankObj_2);
            newObj2.transform.parent = Instance.transform;
            newObj2.gameObject.SetActive(false);
            rankObjQ.Add(2, newObj2);

            var newObj3 = Instantiate(rankObj_3);
            newObj3.transform.parent = Instance.transform;
            newObj3.gameObject.SetActive(false);
            rankObjQ.Add(3, newObj3);
        }
        for(int i = 0; i<100; i++)
        {
            var newObj = Instantiate(rankObj_Default);
            newObj.transform.parent = Instance.transform;
            newObj.gameObject.SetActive(false);
            rankObjQ.Add(4, newObj);
        }


    }



    public static GameObject GetrankObj(int index)
    {
        if (Instance.rankObjQ[index].Count > 0)
        {
            var obj = Instance.rankObjQ.Removeit(index);

            obj.gameObject.SetActive(true);

            return obj;
        }
        else
        {
            Instance.CreateNewrankObj();
            var newObj = Instance.rankObjQ.Removeit(index);

            newObj.gameObject.SetActive(true);

            return newObj;
        }
    }

    public static void ReturnRankObj(int index,GameObject Obj)
    {
        Obj.gameObject.SetActive(false);
        Instance.rankObjQ.Add(index, Obj);
    }
    #endregion

    private void Update()
    {
        UpdateText();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (rankPanel.activeSelf)
            {
                rankPanel.SetActive(false);
            }
            else if (Inventory.activeSelf)
            {
                if (SkillPanel.activeSelf)
                {
                    SkillPanel.SetActive(false);

                }
                else
                {
                    Inventory.SetActive(false);
                }
            }
            else
            {
                isQuitPanel = !isQuitPanel;
                quitPanel.SetActive(isQuitPanel);
            }
        }
    }

    public void UpdateText()
    {
        InvenCoin.text = UserDataManager.user.coin.ToString();
        Coin.text = UserDataManager.user.coin.ToString();
        if (Inventory.activeSelf)
        {
            Characterlvl.text = "Lv." + UserDataManager.user.character_level[InvenCharacter.GetComponent<InvenChar>().CharKey.ToString()].ToString("0");
            CharUpgradeCost.text = InvenCharacter.GetComponent<InvenChar>().UpgradeCost.ToString();
        }
    }   

    #region 스타트 버튼 이벤트
    public void PointerDownStartButton()
    {
        StartButton.sprite = OnStartButton;
    }

    public void PointerUpStartButton()
    {
        StartButton.sprite = OffStartButton;
    }
    #endregion

    #region 버튼 이벤트
    public void PointerDownInventoryButton(GameObject Icon)
    {
        InventoryAnim.SetTrigger("down");
        InventoryAnim.SetBool("IsClick", true);
        Icon.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
    }

    public void PointerUpInventoryButton(GameObject Icon)
    {
        InventoryAnim.SetTrigger("up");
        InventoryAnim.SetBool("IsClick", false);
        Icon.transform.localScale = new Vector3(1f, 1f, 1f);
    }


    #endregion

    #region 스킬 판넬
    public void OnClickSkillStatButton()
    {
        SkillDescPanel.SetActive(false);
        SkillStatPanel.SetActive(true);
        SkillDescButton.GetComponent<Image>().color = new Color32(150, 150, 150, 255);
        SkillStatButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        SoundManager.Instance.SFXPlay("버튼 클릭",ClickButton);
    }

    public void OnClickSkillDescButton()
    {
        SkillStatPanel.SetActive(false);
        SkillDescPanel.SetActive(true);
        SkillStatButton.GetComponent<Image>().color = new Color32(150, 150, 150, 255);
        SkillDescButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        SoundManager.Instance.SFXPlay("버튼 클릭", ClickButton);
    }
    public void OpenSkillPanel(GameObject Skill)
    {
        if (!SkillPanel.activeSelf)
        {
            CurrentSelectedSkill = Skill;
            SkillPanel.SetActive(true);
            OnClickSkillStatButton();

        }
        else
        {
            var i = CurrentSelectedSkill.GetComponent<SkillButton>();
            i.SetButton(i.SKey);
        }
        LoadData(Skill);
        SkillPanel.GetComponent<SkillPanel>().LoadSkillData();
        GameManager.Instance.StatSetting();
        SkillStatPanel.SetActive(true);
        //Characterlvl = InvenCharacter.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        //CharUpgradeCost = InvenCharacter.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();
    }

    private void LoadData(GameObject Skill)
    {
        var Get = SkillManager.Instance;
        var Set = Skill.GetComponent<SkillButton>();
        Get.Name = Set.Name;
        Get.SKey = Set.SKey;
        Get.Command = Set.Command;
        Get.Mana = Set.Mana;
        Get.Dmg = Set.Dmg;
        Get.Ctime = Set.Ctime;
        Get.Description = Set.Description;
        Get.UpgradeCost = Set.UpgradeCost;
        Get.SkillLevel = Set.SkillLevel;
    }

    public void CloseSkillPanel()
    {
        SoundManager.Instance.SFXPlay("버튼 클릭", ClickButton);
        SkillPanel.SetActive(false);
        GameManager.Instance.StatSetting();
        SkillManager.Instance.InitData();
        CreateButton();
    }

    public void OnClickExitPanelButton()
    {
        SkillPanel.SetActive(false);
        SoundManager.Instance.SFXPlay("버튼 클릭", ClickButton);
    }
    #endregion


    public void CreateButton()
    {
        for (int i = 0; i < SkillObjParent.childCount; i++)
        {
            Destroy(SkillObjParent.GetChild(i).gameObject);
        }

        foreach (var i in DataBaseManager.Instance.tdSkillDict.Values)
        {
            Debug.Log("In");
            GameObject Create = (GameObject)Instantiate(SkillButtonObj);
            Create.transform.parent = SkillObjParent;
            Create.transform.localScale = new Vector3(1, 1, 1);
            var j = Create.GetComponent<SkillButton>();
            // 스킬 값 추가
            j.SetButton(i.SKey);

            Debug.Log("Get Static Data");
            //j.SkillLevel = UserDataManager.user.skill_level[DataBaseManager.Instance.tdSkillDict[(int)i.Value.SKey].Name];
            Debug.Log("Create : " + Create.name);
            Create.name = i.Name;
            if (j.SkillLevel < 1)
                Create.GetComponent<Image>().sprite = Resources.Load<Sprite>("SkillIcon/" + "10000") as Sprite;
            else
                Create.GetComponent<Image>().sprite = Resources.Load<Sprite>("SkillIcon/" + i.SKey) as Sprite;
            Debug.Log("Load Sprite Successful!");

        }
    }

    public void OnClickUpgradeButton()
    {
        var skillbutton = CurrentSelectedSkill.GetComponent<SkillButton>();
        skillbutton.Upgrade();
        GameManager.Instance.StatSetting();
        skillbutton.SetButton(skillbutton.SKey);
        SkillPanel.GetComponent<SkillPanel>().LoadSkillData();
        LoadData(CurrentSelectedSkill);
        OpenSkillPanel(CurrentSelectedSkill);
        UserDataManager.Instance.Save();
    }

    public void OnClickCharacterUpGradeButton()
    {
        var invn = InvenCharacter.GetComponent<InvenChar>();
        invn.Upgrade();
        GameManager.Instance.CharSetting();
        invn.LoadData(invn.CharKey);
        UserDataManager.Instance.Save();
        Debug.Log("Successful Save Data! UpgradeCost : " + invn.UpgradeCost);
    }

    public void OnClickLeaveDoor()
    {
        StartCoroutine(StartScene());
    }

    public void OnClickUpgradeChar() 
    {
        OnClickCharacterUpGradeButton();
    }

    public void OnClickSoundButton()
    {
        SoundManager.Instance.SFXPlay("버튼 클릭", ClickButton);
        SoundPanel.SetActive(true);
    }

    public void Tutorial()
    {
        if (!TutorialPanel.activeSelf)
        {
            TutorialPanel.SetActive(true);
        }
        TutorialImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("tutorial/" + TutorialNumber.ToString()) as Sprite;
    }
    
    public void TutorialLeftArrow()
    {
        if (TutorialNumber > 1)
        {
            TutorialNumber--;
            Tutorial();
        }
    }

    public void TutorialRightArrow()
    {
        if (TutorialNumber > 4)
        {
            TutorialPanel.SetActive(false);
        }
        else
        {
            TutorialNumber++;
            Tutorial();
        }
    }
    public void BGSoundCheckBox()
    {
        if (BG_Check.transform.GetChild(0).gameObject.activeSelf && PlayerPrefs.GetFloat("BGSound") == 0f)
        {
            BG_Check.transform.GetChild(0).gameObject.SetActive(false);
            PlayerPrefs.SetFloat("BGSound", -80f);
        }
        else
        {
            BG_Check.transform.GetChild(0).gameObject.SetActive(true);
            PlayerPrefs.SetFloat("BGSound", 0f);
        }
        Debug.Log("BGSound : " + PlayerPrefs.GetFloat("BGSound"));
    }

    public void SFXCheckBox()
    {
        if (SFX_Check.transform.GetChild(0).gameObject.activeSelf && PlayerPrefs.GetFloat("SFX")==0f)
        {
            SFX_Check.transform.GetChild(0).gameObject.SetActive(false);
            PlayerPrefs.SetFloat("SFX", -80f);
        }
        else
        {
            SFX_Check.transform.GetChild(0).gameObject.SetActive(true);
            PlayerPrefs.SetFloat("SFX", 0f);
        }
        
        Debug.Log("SFX : " + PlayerPrefs.GetFloat("SFX"));
    }

    public void ExitSoundPanel(GameObject SoundPanel)
    {
        SoundPanel.SetActive(false);
        SoundManager.Instance.SFXPlay("버튼 클릭", ClickButton);
    }
}
