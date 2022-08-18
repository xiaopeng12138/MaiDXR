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
                Time.fixedDeltaTime = 1 / 30;
                break;
            case 1:
                Time.fixedDeltaTime = 1 / 60;
                break; 
            case 2:
                Time.fixedDeltaTime = 1 / 90;
                break;
            case 3:
                Time.fixedDeltaTime = 1 / 120;
                break;
            case 4:
                Time.fixedDeltaTime = 1 / 140;
                break;
            case 5:
                Time.fixedDeltaTime = 1 / 160;
                break;
            case 6:
                Time.fixedDeltaTime = 1 / 180;
                break;
            case 7:
                Time.fixedDeltaTime = 1 / 200;
                break;
        }
        JsonConfig.SetInt("TouchFPS", Dropdown.value);
    }
}
