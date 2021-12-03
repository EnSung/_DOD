using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DOD.Define;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public GameObject inputName;
    public Text overlapName;

    public GameObject InvenCharacter;

    /// <summary>
    /// 로비로
    /// </summary>
    public void ToLobbyScene()
    {
        SoundManager.Instance.SFXPlay("버튼 클릭", GameSceneUIManager.Instance.ClickButton);
        LoadingSceneController.LoadScene(Scenes.LobbyScene.ToString());
        Time.timeScale = 1;
   }

    public void TitleToLobby()
    {
        LoadingSceneController.TitletoLoadScene(Scenes.LobbyScene.ToString());
        Time.timeScale = 1;
    }


    /// <summary>
    /// 게임씬으로
    /// </summary>
    public void ToGameScene()
    {
        if(SceneManager.GetActiveScene().name == "GameScene")
        {
            SoundManager.Instance.SFXPlay("버튼 클릭", GameSceneUIManager.Instance.ClickButton);
        }
        else if (SceneManager.GetActiveScene().name == "LobbyScene")
        {
            SoundManager.Instance.SFXPlay("버튼 클릭", LobbyUIManager.Instance.ClickButton);
        }
        LoadingSceneController.LoadScene(Scenes.GameScene.ToString());
        GameManager.Instance.StatSetting();
        GameManager.Instance.CharSetting();
        Time.timeScale = 1;
        UserDataManager.Instance.Save();

    }

    // 로비
    public void OpenInventory()
    {
        SoundManager.Instance.SFXPlay("버튼 클릭", LobbyUIManager.Instance.ClickButton);
        Debug.Log(1);
        var i = LobbyUIManager.Instance;
        Debug.Log(2);
        i.Inventory.SetActive(true);
        Debug.Log(3);
        GameManager.Instance.CharSetting();
        Debug.Log(4);
        GameManager.Instance.StatSetting();
        Debug.Log(5);
        i.CreateButton();
        Debug.Log(6);
        i.InvenCharacter.GetComponent<InvenChar>().LoadData(i.InvenCharKey);
        Debug.Log(7);
    }

    public void CloseInventory()
    {
        SoundManager.Instance.SFXPlay("버튼 클릭", LobbyUIManager.Instance.ClickButton);
        LobbyUIManager.Instance.Inventory.SetActive(false);
        Debug.Log("Close Inventory Panel");
        GameManager.Instance.StatSetting();

    }

    /// <summary>
    /// 오브젝트를 보여주는함수
    /// </summary>
    /// <param name="viewObj">보여줄 오브젝트</param>
    public void Viewit(GameObject viewObj)
    {
        SoundManager.Instance.SFXPlay("버튼 클릭", LobbyUIManager.Instance.ClickButton);
        viewObj.SetActive(true);
    }
    /// <summary>
    /// 오브젝트를 끄는함수
    /// </summary>
    /// <param name="viewObj">끌 오브젝트</param>
    public void Hideit(GameObject viewObj)
    {
        SoundManager.Instance.SFXPlay("버튼 클릭", LobbyUIManager.Instance.ClickButton);
        viewObj.SetActive(false);
    }

    public void enter_nickName(Text text)
    {
        SoundManager.Instance.SFXPlay("버튼 클릭", LobbyUIManager.Instance.ClickButton);
        if (text.text != "")
        {
            UserDataManager.user.nickname = text.text;
            Debug.Log(UserDataManager.user.nickname);
            UserDataManager.Instance.Sendnick();
            Debug.Log("닉넴 입력");
            Invoke("nickNameCheck", 1f);
        }
        Debug.Log(UserDataManager.user.nickname);
    }

    public void nickNameCheck()
    {
        if (UserDataManager.Instance.nickExist)
        {
            Debug.Log("닉네임 중복");
            StartCoroutine(OverlapName());
        }
        else
        {
            inputName.SetActive(false);
            GameManager.Instance.isnameInput = true;
            UserDataManager.Instance.Init();
            InvenCharacter.GetComponent<InvenChar>().LoadData(30001);


        }
    }
    
    IEnumerator OverlapName()
    {
        overlapName.gameObject.SetActive(true);
        while (true) {
            yield return null;
            overlapName.color = new Color(overlapName.color.r, overlapName.color.g, overlapName.color.b, overlapName.color.a - 0.01f);
            if (overlapName.color.a <= 0)
            {
                overlapName.gameObject.SetActive(false);
                overlapName.color = overlapName.color = new Color(overlapName.color.r, overlapName.color.g, overlapName.color.b, 1);
                break;
            }
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    //겜씬


    /// <summary>
    /// 게임씬에서 멈출 때 사용
    /// </summary>
    /// <param name="viewObject">pausePanel</param>
    public void Pause(GameObject viewObject)
    {
        SoundManager.Instance.SFXPlay("버튼 클릭", GameSceneUIManager.Instance.ClickButton);
        Time.timeScale = 0;
        viewObject.SetActive(true);
    }


    /// <summary>
    /// 게임씬에서 재시작할때 사용
    /// </summary>
    /// <param name="viewObject">pausePanel</param>
    public void Pose(GameObject viewObject)
    {
        SoundManager.Instance.SFXPlay("버튼 클릭", GameSceneUIManager.Instance.ClickButton);
        Time.timeScale = 1;
        viewObject.SetActive(false);
    }


    /// <summary>
    /// 게임씬을 중간에 나갈때 사용
    /// </summary>
    /// <param name="viewObject">pasuePanel</param>
    public void gameSceneQuit(GameObject viewObject)
    {
        SoundManager.Instance.SFXPlay("버튼 클릭", GameSceneUIManager.Instance.ClickButton);
        Time.timeScale = 1;
        viewObject.SetActive(false);
        BattleManager.Instance.isEnd = true;
        UserDataManager.Instance.Save();
    }


}
    