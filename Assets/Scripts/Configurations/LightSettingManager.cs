using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSettingManager : MonoBehaviour
{
    public List<Light> Lights;
    public void OnStateChanges(bool isOn)
    {
        foreach (var light in Lights)
        {
            light.enabled = isOn;
        }
    }
}
