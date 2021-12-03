using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DOD.DB;

public class TipText : MonoBehaviour
{
    Text tip;

    void Awake()
    {
        tip = GetComponent<Text>();
    }
    void Start()
    {
        tip.text = "tip. " + DataBaseManager.Instance.tipText[Random.Range(0, DataBaseManager.Instance.tipText.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
