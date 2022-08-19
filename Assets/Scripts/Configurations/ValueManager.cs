using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ValueManager : MonoBehaviour
{
    TMP_Text tmp;
    public float Value;
    float tempValue;
    public bool isPointerDown = false;
    void Start()
    {
        tmp = GetComponent<TMP_Text>();
    }
    void Update()
    {
        if (isPointerDown)
        {
            ChangeValueContinue(tempValue);
        }
    }

    public void ChangeValueContinue(float value)
    {
        tempValue = value;
        Value += Time.deltaTime * value;
        tmp.text = String.Format("{0:F2}", Value);
        isPointerDown = true;
    }
    public void PointerState(bool state)
    {
        isPointerDown = state;
    }
    public void ChangeValue(float value)
    {
        Value += value;
        tmp.text = String.Format("{0:F2}", Value);
    }
}
