using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffect : MonoBehaviour
{

    void OnEnable()
    {
        Invoke("Destroythis", 0.6f);
    }

    public void Destroythis()
    {
        Objectpool.ReturnhealEffect(this);
    }

}
