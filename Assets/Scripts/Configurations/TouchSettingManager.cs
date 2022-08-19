using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TouchSettingManager : MonoBehaviour
{
    TMP_Dropdown Dropdown;
    void Start()
    {
        Dropdown = GetComponent<TMP_Dropdown>();
        GetTouchFPS();
    }
    void GetTouchFPS()
    {
        if (JsonConfig.HasKey("TouchFPS"))
            Dropdown.value = JsonConfig.GetInt("TouchFPS");
        SetTouchFPS();
    }
    public void SetTouchFPS()
    {
        switch (Dropdown.value)
        {
            case 0:
                Time.fixedDeltaTime = 1 / 30f;
                break;
            case 1:
                Time.fixedDeltaTime = 1 / 60f;
                break; 
            case 2:
                Time.fixedDeltaTime = 1 / 90f;
                break;
            case 3:
                Time.fixedDeltaTime = 1 / 120f;
                break;
            case 4:
                Time.fixedDeltaTime = 1 / 140f;
                break;
            case 5:
                Time.fixedDeltaTime = 1 / 160f;
                break;
            case 6:
                Time.fixedDeltaTime = 1 / 180f;
                break;
            case 7:
                Time.fixedDeltaTime = 1 / 200f;
                break;
        }
        JsonConfig.SetInt("TouchFPS", Dropdown.value);
    }
}
