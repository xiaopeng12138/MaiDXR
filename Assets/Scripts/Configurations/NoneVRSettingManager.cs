using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoneVRSettingManager : MonoBehaviour
{
    public Camera FPCamera;
    public Camera TPCamera;
    private CameraSmooth CameraSmooth;
    private TMP_Dropdown Dropdown;
    private Slider Slider;
    void Start()
    {
        CameraSmooth = FPCamera.GetComponent<CameraSmooth>();
        Dropdown = GetComponent<TMP_Dropdown>();
        Slider = GetComponent<Slider>();
        switch (gameObject.name)
        {
            case "NVRModeDropdown":

                break;
            case "NVRFOV":

                break;
            case "NVRFPSDropdown":

                break;
        }
        
    }

    public void GetNVRMode()
    {
        if (JsonConfig.HasKey("NVRMode"))
            Dropdown.value = JsonConfig.GetInt("NVRMode");
        SetNVRMode();
    }
    public void GetNVRFOV()
    {
        if (JsonConfig.HasKey("NVRFOV"))
            Slider.value = (float)JsonConfig.GetDouble("NVRFOV");
        SetNVRFOV();
    }
    public void GetNVRFPSDropdown()
    {
        if (JsonConfig.HasKey("NVRFPS"))
            Dropdown.value = JsonConfig.GetInt("NVRFPS");
        SetNVRFPSDropdown();
    }
    
    public void SetNVRMode()
    {
        switch (Dropdown.value)
        {
            case 0:
                FPCamera.enabled = false;
                TPCamera.enabled = false;
                break;
            case 1:
                FPCamera.enabled = true;
                TPCamera.enabled = false;
                break;
            case 2:
                FPCamera.enabled = false;
                TPCamera.enabled = true;
                break;
        }
        JsonConfig.SetInt("NVRMode", Dropdown.value);
    }
    public void SetNVRFOV()
    {
        FPCamera.fieldOfView = Slider.value;
        TPCamera.fieldOfView = Slider.value;
        JsonConfig.SetDouble("NVRFOV", Slider.value);
    }
    public void SetNVRFPSDropdown()
    {
        switch (Dropdown.value)
        {
            case 0:
                CameraSmooth.FPS = 15;
                break;
            case 1:
                CameraSmooth.FPS = 30;
                break; 
            case 2:
                CameraSmooth.FPS = 45;
                break;
            case 3:
                CameraSmooth.FPS = 60;
                break;
            case 4:
                CameraSmooth.FPS = 90;
                break;
            case 5:
                CameraSmooth.FPS = 120;
                break;
            case 6:
                CameraSmooth.FPS = 144;
                break;
        }
        JsonConfig.SetInt("NVRFPS", Dropdown.value);
    }
}
