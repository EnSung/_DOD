using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public virtual void SetText<T>(Text text, T Content)
    {
        text.text = Content.ToString();
    }
}
