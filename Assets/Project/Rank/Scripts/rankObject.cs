using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rankObject : MonoBehaviour
{
    GameObject Contents;
    public Text scoreText;
    public Text rankText;
    public Text nicknameText;

    RectTransform RT;
    public int index;

    /*public void Awake()
    {
        RT = GetComponent<RectTransform>();
        Debug.Log(RT.sizeDelta.x);
        Debug.Log(RT.sizeDelta.y);

        if (Camera.main.rect.y != 0)
        {
            RT.sizeDelta = new Vector2(RT.sizeDelta.x * ((Screen.width * Camera.main.rect.y) / (float)1280), RT.sizeDelta.y * ((Screen.height * Camera.main.rect.height) / (float)1280));
        }
        else
        {
            RT.sizeDelta = new Vector2(RT.sizeDelta.x * (Screen.width / (float)1280), RT.sizeDelta.y * ((Screen.height * Camera.main.rect.height) / (float)1280));
        }

        Debug.Log(RT.sizeDelta.x);
        Debug.Log(RT.sizeDelta.y);
    }*/
    public void setData(string rank, string Name, string Score)
    {
        rankText.text = rank.ToString();
        nicknameText.text = Name;
        scoreText.text = Score.ToString();
    }
}
