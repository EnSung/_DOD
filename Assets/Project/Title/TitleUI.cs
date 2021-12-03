using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DOD.Define;

public class TitleUI : MonoBehaviour
{
    public Text loadingText;
    public GameObject tapTostart;
    public Slider loadingBar;

    public void SetLoadStateDescription(IntroPhase loadState)
    {
        switch (loadState)
        {
            case IntroPhase.Start:
                loadingText.text = "길을 찾는 중입니다...";
                break;
            case IntroPhase.ApplicationSetting:
                loadingText.text = "장비를 점검 중입니다...";
                break;
            case IntroPhase.StaticData:
                loadingText.text = "마나를 채우는 중입니다...";
                break;
            case IntroPhase.UserData:
                loadingText.text = "적들에 대한 정보를 조사하는 중입니다...";
                break;
            case IntroPhase.Compelte:
                loadingText.text = "";
                break;
            default:
                break;
        }
    }
        
    public IEnumerator LoadGaugeUpdate(float loadPer)
    {
        while (!Mathf.Approximately(loadingBar.value, loadPer))
        {
            loadingBar.value = Mathf.Lerp(loadingBar.value, loadPer, Time.deltaTime * 2f);

            yield return null;
        }
    }


    public IEnumerator setComplete()
    {

        yield return new WaitForSeconds(0.7f);
        loadingBar.gameObject.SetActive(false);
        tapTostart.SetActive(true);

    }

}
