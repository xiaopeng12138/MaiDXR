using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoneVRSettingManager : MonoBehaviour
{
    public GameObject NVRCameraObj;
    public Camera NVRCamera;
    public Transform NVRCameraTargetFP;
    public Transform NVRCameraTargetTP;
    public CameraSmooth CameraSmooth;
    private TMP_Dropdown Dropdown;
    private Slider Slider;
    void Start()
    {
        Dropdown = GetComponent<TMP_Dropdown>();
        Slider = GetComponent<Slider>();
        switch (gameObject.name)
        {
            case "NVRModeDropdown":
                GetNVRMode();
                break;
            case "NVRFOV":
                GetNVRFOV();
                break;
            case "NVRFPSDropdown":
                GetNVRFPS();
                break;
            case "TPCameraCube":
                GetTPCamTransform();
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
        SetNVRFOV(Slider.value);
    }
    public void GetNVRFPS()
    {
        QualitySettings.vSyncCount = 0;
        if (JsonConfig.HasKey("NVRFPS"))
            Dropdown.value = JsonConfig.GetInt("NVRFPS");
        SetNVRFPS();
    }
    public void GetTPCamTransform()
    {
        if (JsonConfig.HasKey("TPCamPosition"))
            gameObject.transform.position = JsonConfig.GetVector3("TPCamPosition");
        if (JsonConfig.HasKey("TPCamRotation"))
            gameObject.transform.rotation = JsonConfig.GetQuaternion("TPCamRotation");
            
        SetTPCamTransform();
    }
    
    public void SetNVRMode()
    {
        switch (Dropdown.value)
        {
            case 0:
                if (NVRCameraObj.activeSelf)
                    NVRCameraObj.SetActive(false);
                break;
            case 1:
                if (!NVRCameraObj.activeSelf)
                    NVRCameraObj.SetActive(true);
                CameraSmooth.target = NVRCameraTargetFP;
                break;
            case 2:
                if (!NVRCameraObj.activeSelf)
                    NVRCameraObj.SetActive(true);
                CameraSmooth.target = NVRCameraTargetTP;
                break;
        }
        JsonConfig.SetInt("NVRMode", Dropdown.value);
    }
    public void SetNVRFOV(float fov)
    {
        NVRCamera.fieldOfView = fov;
        JsonConfig.SetDouble("NVRFOV", fov);
    }
    public void SetNVRFPS()
    {
        switch (Dropdown.value)
        {
            case 0:
                Application.targetFrameRate = 15;
                break;
            case 1:
                Application.targetFrameRate = 30;
                break; 
            case 2:
                Application.targetFrameRate = 45;
                break;
            case 3:
                Application.targetFrameRate = 60;
                break;
            case 4:
                Application.targetFrameRate = 90;
                break;
            case 5:
                Application.targetFrameRate = 120;
                break;
            case 6:
                Application.targetFrameRate = 144;
                break;
        }
        JsonConfig.SetInt("NVRFPS", Dropdown.value);
    }
    public void SetTPCamTransform()
    {
        JsonConfig.SetVector3("TPCamPosition", gameObject.transform.position);
        JsonConfig.SetQuaternion("TPCamRotation", gameObject.transform.rotation);
    }
}
