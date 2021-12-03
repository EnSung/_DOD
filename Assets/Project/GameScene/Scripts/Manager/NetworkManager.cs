using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : Singleton<NetworkManager>
{
    public GameObject Network;
    private GameObject Exit;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void CheckNetwork()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable && !Network.activeSelf)
        {
            Network.SetActive(true);
        }
    } 
}
