using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DOD.DB;
using DOD.Define;
public class TitleController : MonoBehaviour
{
    /// <summary>
    /// 현재 페이즈의 완료 상태를 나타냄
    /// </summary>
    private bool loadComplete;

    /// <summary>
    /// 외부에서 loadComplete에 접근하기 위한 프로퍼티
    /// 추가로 현재로 페이즈 완료 시 조건에 따라 다음 페이즈로 변경
    /// </summary>
    public bool LoadComplete
    {
        get => loadComplete;
        set
        {
            loadComplete = value;
            if (loadComplete && !allLoaded)
                NextPhase();
        }
    }

    /// <summary>
    /// 모든 페이즈의 완료 상태를 나타냄
    /// </summary>
    private bool allLoaded;

    IntroPhase phase = IntroPhase.Start;

    public TitleUI UI;


    Coroutine loadGaugeUpdateCorutine;

    public void Initialize()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("WIFI ERROR");
        }
        OnPhase(phase);
    }
    private void OnPhase(IntroPhase phase)
    {
        
        UI.SetLoadStateDescription(phase);
        if (loadGaugeUpdateCorutine != null)
        {
            StopCoroutine(loadGaugeUpdateCorutine);
            loadGaugeUpdateCorutine = null;
        }
        if (phase != IntroPhase.Compelte)
        {
            var loadPer = (float)phase / (float)IntroPhase.Compelte;
            loadGaugeUpdateCorutine =  StartCoroutine(UI.LoadGaugeUpdate(loadPer));
        }
        else
        {
            UI.loadingBar.value = 1f;
        }
        switch (phase)
        {
            case IntroPhase.Start:
                LoadComplete = true;
                Debug.Log(phase + "Complete");
                break;
            case IntroPhase.ApplicationSetting:
                GameManager.Instance.ApplicationSetting();
                LoadComplete = true;
                Debug.Log(phase + "Complete");
                break;
            case IntroPhase.StaticData:
                DataBaseManager.Instance.LoadTable();
                LoadComplete = true;
                Debug.Log(phase + "Complete");
                break;
            case IntroPhase.UserData:
                //UserDataManager.Instance.DeviceCheck();
                UserDataManager.Instance.Init();
                LoadComplete = true;
                Debug.Log(phase + "Complete");
                break;
            case IntroPhase.Compelte:
                allLoaded = true;
                LoadComplete = true;
                Debug.Log(phase + "Complete");
                StartCoroutine(UI.setComplete());
                break;
        }
    }


    private void NextPhase()
    {
        StartCoroutine(WaitForSeconds());

        IEnumerator WaitForSeconds()
        {
            yield return new WaitForSeconds(1f);

            
            loadComplete = false;
            OnPhase(++phase);
        }
    }



    
}
