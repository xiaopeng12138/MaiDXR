using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using WindowsInput.Native;

public class ButtonSettingManager : MonoBehaviour
{
    public ButtonToKey Button;
    TMP_Dropdown Dropdown;
    void Start()
    {
        Dropdown = GetComponent<TMP_Dropdown>();
        PopulateList();
        GetKeyCode();
    }
    void GetKeyCode()
    {
        if (JsonConfig.HasKey(gameObject.name))
            Dropdown.value = JsonConfig.GetInt(gameObject.name);
        OnValueChanged(Dropdown.value);
    }
    public void OnValueChanged(int value)
    {
        Button.keyToPress = (VirtualKeyCode)Enum.GetValues(typeof(VirtualKeyCode)).GetValue(value);
    }
    void PopulateList()
    {
        string[] enumNames = Enum.GetNames(typeof(VirtualKeyCode));
        List<string> keyNames = new List<string>(enumNames);
        Dropdown.AddOptions(keyNames);
    }
}
