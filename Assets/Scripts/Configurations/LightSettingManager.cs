using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSettingManager : MonoBehaviour
{
    public List<GameObject> Lights;
    public void OnStateChanges(bool isOn)
    {
        foreach (var light in Lights)
        {
            light.SetActive(isOn);
        }
    }
}
