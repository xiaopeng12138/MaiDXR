using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class HapticSettingManager : MonoBehaviour
{
    public List<Controllers> Controllers;
    Slider Slider;
    void Start()
    {
        Slider = GetComponent<Slider>();
        switch (gameObject.name)
        {
            case "HpDuration":
                GetHapticDuration();
                break;
            case "HpAmplitude":
                GetHapticAmplitude();
                break;
        }
    }
    void GetHapticDuration()
    {
        if (JsonConfig.HasKey("HapticDuration"))
            Slider.value = (float)JsonConfig.GetDouble("HapticDuration");
        SetHapticDuration(Slider.value);
    }
    void GetHapticAmplitude()
    {
        if (JsonConfig.HasKey("HapticAmplitude"))
            Slider.value = (float)JsonConfig.GetDouble("HapticAmplitude") * 10;
        SetHapticAmplitude(Slider.value);
    }

    public void SetHapticDuration(float duration)
    {
        foreach (var controller in Controllers)
        {
            controller.duration = duration;
        }
        JsonConfig.SetDouble("HapticDuration", duration);
    }
    public void SetHapticAmplitude(float amplitude)
    {
        amplitude /= 10;
        foreach (var controller in Controllers)
        {
            controller.amplitude = amplitude;
        }
        JsonConfig.SetDouble("HapticAmplitude", amplitude);
    }
}
