using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DOD.Define;

public class tapTostart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Ended)
            {
                Debug.Log("click!");
                LoadingSceneController.LoadScene(Scenes.LobbyScene.ToString());
            }

        }
    }
}
