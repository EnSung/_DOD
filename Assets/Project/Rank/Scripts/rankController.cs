using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DOD.DB;
public class rankController : MonoBehaviour
{
    public Transform rankObjParent;

    public void setRank()
    {
        DataBaseManager.Instance.LoadRankData();
        //rank();
        Invoke("rank", 0.1f);
    }

    public void rank()
    {
        Debug.Log("랭크 오브젝트 생성");


        for (int i = 0; i < rankObjParent.childCount; i++)
        {
            LobbyUIManager.ReturnRankObj(rankObjParent.GetChild(i).gameObject.GetComponent<rankObject>().index, rankObjParent.GetChild(i).gameObject);
            Debug.Log(rankObjParent.GetChild(i).name);
        }

        GameObject obj;
        for (int i = 0; i < DataBaseManager.Instance.userRankDict.Count; i++)
        {
            switch (i + 1)
            {
                case 1:
                    obj = LobbyUIManager.GetrankObj(1);
                    break;
                case 2:
                    obj = LobbyUIManager.GetrankObj(2);
                    break;
                case 3:
                    obj = LobbyUIManager.GetrankObj(3);
                    break;
                default:
                    obj = LobbyUIManager.GetrankObj(4);
                    break;
            }
            obj.transform.parent = rankObjParent.transform;
            obj.GetComponent<rankObject>().setData((i + 1).ToString(), DataBaseManager.Instance.userRankDict[i].nickname, DataBaseManager.Instance.userRankDict[i].score);

        }
    }
}
