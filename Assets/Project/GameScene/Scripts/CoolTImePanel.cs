using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolTImePanel : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (transform.childCount != 0)
        {
            for (int i = 0; i <= transform.childCount; i++)
            {
                transform.GetChild(i).localScale = new Vector2(transform.localScale.x - 5, transform.localScale.x - 5);
            }
        }
    }
}
