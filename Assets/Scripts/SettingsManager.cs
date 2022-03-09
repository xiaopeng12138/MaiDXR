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
    public Material ScreenMaterial;
    public GameObject SmoothCameraObj;
    public Camera SmoothCamera;
    public GameObject XROriginObj;
    public GameObject[] ButtonObjs;
    public GameObject SelectButton;    
    public GameObject HeadCube;
    private void Awake()
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
            }  
        }
        if (!Application.isFocused)
            FocusChecked=false;
    }
    void UpdateFromFile()
    {
        LHandObj.transform.localScale = new Vector3(Setting.HandSize/100,Setting.HandSize/100,Setting.HandSize/100);
        RHandObj.transform.localScale = new Vector3(Setting.HandSize/100,Setting.HandSize/100,Setting.HandSize/100);
        LHandObj.transform.localPosition = new Vector3(Setting.HandPosition[0]/100,Setting.HandPosition[1]/100,Setting.HandPosition[2]/100);
        RHandObj.transform.localPosition = new Vector3(Setting.HandPosition[0]/-100,Setting.HandPosition[1]/100,Setting.HandPosition[2]/100);
        XROrigin XROriginScp = XROriginObj.GetComponent<XROrigin>();
        XROriginScp.CameraYOffset = Setting.PlayerHigh;
        UwcWindowTexture ScreenScp = ScreenObj.GetComponent<UwcWindowTexture>();
        ScreenScp.captureFrameRate = Setting.CaptureFrameRate;
        ScreenMaterial.SetTextureScale("_MainTex",new Vector2(Setting.Capture1PlayerOnly ? 1f : 0.5f, 1));
        CameraSmooth CameraSmoothScp = SmoothCameraObj.GetComponent<CameraSmooth>();
        CameraSmoothScp.smoothSpeed = Setting.CameraSmooth;
        SmoothCamera.fieldOfView = Setting.CameraFOV;
        CameraSmoothScp.PositionOffset = new Vector3(Setting.CameraPosition[0],Setting.CameraPosition[1],Setting.CameraPosition[2]);
        MeshRenderer HeadCubeMesh = HeadCube.GetComponent<MeshRenderer>();
        HeadCubeMesh.enabled = Setting.ShowHeadCube;
        Controller LHandScp = LHandObj.GetComponent<Controller>();
        LHandScp.amplitude = Setting.HapticAmplitude;
        Controller RHandScp = RHandObj.GetComponent<Controller>();
        RHandScp.amplitude = Setting.HapticAmplitude;
        XROriginScp.CameraYOffset = Setting.PlayerHigh/100;
        Time.fixedDeltaTime = 1/Setting.TouchRefreshRate;
        ButtonToKey SelectButtonScp = SelectButton.GetComponent<ButtonToKey>();
        SelectButtonScp.keyToPress = (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), Setting.SelectButton);
        ButtonToKey Button1Scp = ButtonObjs[0].GetComponent<ButtonToKey>();
        Button1Scp.keyToPress = (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), Setting.Button1);
        ButtonToKey Button2Scp = ButtonObjs[1].GetComponent<ButtonToKey>();
        Button2Scp.keyToPress = (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), Setting.Button2);
        ButtonToKey Button3Scp = ButtonObjs[2].GetComponent<ButtonToKey>();
        Button3Scp.keyToPress = (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), Setting.Button3);
        ButtonToKey Button4Scp = ButtonObjs[3].GetComponent<ButtonToKey>();
        Button4Scp.keyToPress = (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), Setting.Button4);
        Debug.Log("Setting Updated");
    }

    void FirstStart()
    {
        Debug.Log("FirstStart");
        Debug.Log("Append Setting File");
        JsonPath = Path.GetDirectoryName(Application.dataPath) + "/Settings.json";
        Debug.Log(JsonPath);
        if (!File.Exists (JsonPath))
        {
            Settings Setting = new Settings()
            {
                HandSize = 8f,
                HandPosition = new float[3]{2f, -2f, 7f},
                PlayerHigh = 180f,
                CaptureFrameRate = 90,
                Capture1PlayerOnly = false,
                TouchRefreshRate = 120,
                CameraSmooth = 0.05f,
                CameraFOV = 80f,
                CameraPosition = new float[3]{0f, 0f, 0f},
                ShowHeadCube = false,
                HapticDuration = 0.2f,
                HapticAmplitude = 1f,
                SelectButton = "VK_3",
                Button1 = "SCROLL",
                Button2 = "PAUSE",
                Button3 = "VK_1",
                Button4 = "VK_2"
            };
            JsonStr = JsonConvert.SerializeObject(Setting, Formatting.Indented);
            Debug.Log(JsonStr);
            File.AppendAllText(JsonPath, JsonStr);
            Debug.Log("Setting FileAppended");
        }
            Debug.Log("Read Setting File");
            JsonStr = File.ReadAllText(JsonPath);
            Setting = JsonConvert.DeserializeObject<Settings>(JsonStr);
            Debug.Log("Setting File Readed");
    }
    
}
public class Settings
{
    public float HandSize { get; set; }
    public float[] HandPosition { get; set; }
    public float PlayerHigh { get; set; }
    public int CaptureFrameRate { get; set; }
    public bool Capture1PlayerOnly { get; set; }
    public float TouchRefreshRate { get; set; }
    public float CameraSmooth { get; set; }
    public float CameraFOV { get; set; }
    public float[] CameraPosition { get; set; }
    public bool ShowHeadCube { get; set; }
    public float HapticDuration { get; set; }
    public float HapticAmplitude { get; set; }
    public string SelectButton { get; set; }
    public string Button1 { get; set; }
    public string Button2 { get; set; }
    public string Button3 { get; set; }
    public string Button4 { get; set; }

}