using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Unity.XR.CoreUtils;
using uWindowCapture;
using WindowsInput.Native;
using System;
public class SettingsManager : MonoBehaviour
{
    string JsonPath;
    string JsonStr;
    Settings Setting = new Settings();
    bool FocusChecked = true;
    public GameObject LHandObj;
    public GameObject RHandObj;
    public GameObject ScreenObj;
    public GameObject SmoothCameraObj;
    public Camera SmoothCamera;
    public GameObject XROriginObj;
    public GameObject Button1Obj;    
    void Start()
    {
        FirstStart();
        UpdateFromFile();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5) | !FocusChecked)
        {
            if (Application.isFocused)
            {
                FocusChecked=true;
                JsonStr = File.ReadAllText(JsonPath);
                Setting = JsonConvert.DeserializeObject<Settings>(JsonStr); 
                UpdateFromFile();
                Debug.Log("Setting Updated");
            }  
        }
        if (!Application.isFocused)
            FocusChecked=false;
    }
    void UpdateFromFile()
    {
        LHandObj.transform.localScale = new Vector3(Setting.HandSize/100,Setting.HandSize/100,Setting.HandSize/100);
        RHandObj.transform.localScale = new Vector3(Setting.HandSize/100,Setting.HandSize/100,Setting.HandSize/100);
        LHandObj.transform.localPosition = new Vector3(Setting.HandPositionX/100,Setting.HandPositionY/100,Setting.HandPositionZ/100);
        RHandObj.transform.localPosition = new Vector3(Setting.HandPositionX/-100,Setting.HandPositionY/100,Setting.HandPositionZ/100);
        XROrigin XROriginScp = XROriginObj.GetComponent<XROrigin>();
        XROriginScp.CameraYOffset = Setting.PlayerHigh;
        UwcWindowTexture ScreenScp = ScreenObj.GetComponent<UwcWindowTexture>();
        ScreenScp.captureFrameRate = Setting.CaptureFrameRate;
        CameraSmooth CameraSmoothScp = SmoothCameraObj.GetComponent<CameraSmooth>();
        CameraSmoothScp.smoothSpeed = Setting.CameraSmooth;
        SmoothCamera.fieldOfView = Setting.CameraFOV;
        Controller LHandScp = LHandObj.GetComponent<Controller>();
        LHandScp.amplitude = Setting.HapticAmplitude;
        Controller RHandScp = RHandObj.GetComponent<Controller>();
        RHandScp.amplitude = Setting.HapticAmplitude;
        XROriginScp.CameraYOffset = Setting.PlayerHigh/100;
        Time.fixedDeltaTime = 1/Setting.TouchRefreshRate;
        ButtonToKey Button1Scp = Button1Obj.GetComponent<ButtonToKey>();
        Button1Scp.keyToPress = (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), Setting.Button1);
    }

    void FirstStart()
    {
        JsonPath = Path.GetDirectoryName(Application.dataPath) + "/Settings.json";
        Debug.Log(JsonPath);
        if (!File.Exists (JsonPath))
        {
            Settings Setting = new Settings()
            {
                HandSize = 8f,
                HandPositionX = 2f,
                HandPositionY = -2f,
                HandPositionZ = 7f,
                PlayerHigh = 180f,
                CaptureFrameRate = 90,
                TouchRefreshRate = 90,
                CameraSmooth = 0.1f,
                CameraFOV = 85f,
                HapticDuration = 0.15f,
                HapticAmplitude = 1,
                Button1 = "SCROLL"
            };
            JsonStr = JsonConvert.SerializeObject(Setting, Formatting.Indented);
            Debug.Log(JsonStr);
            File.AppendAllText(JsonPath, JsonStr);
        }
        else
        {
            JsonStr = File.ReadAllText(JsonPath);
            Setting = JsonConvert.DeserializeObject<Settings>(JsonStr);
        }
    }
    
}
public class Settings
{
    public float HandSize { get; set; }
    public float HandPositionX { get; set; }
    public float HandPositionY { get; set; }
    public float HandPositionZ { get; set; }
    public float PlayerHigh { get; set; }
    public int CaptureFrameRate { get; set; }
    public float TouchRefreshRate { get; set; }
    public float CameraSmooth { get; set; }
    public float CameraFOV { get; set; }
    public float HapticDuration { get; set; }
    public float HapticAmplitude { get; set; }
    public string Button1 { get; set; }
    //public string Button2 { get; set; }
    //public string Button3 { get; set; }
    //public string Button4 { get; set; }

}