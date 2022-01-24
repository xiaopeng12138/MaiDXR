using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class SettingsManager : MonoBehaviour
{
    public string JsonPath;
    public string JsonStr;
    public Settings Setting = new Settings();
    void Start()
    {
        JsonPath = Path.GetDirectoryName(Application.dataPath) + "/Settings.json";
        Debug.Log(JsonPath);
        if (!File.Exists (JsonPath))
        {
            Settings Setting = new Settings()
            {
                HandSize = 8.5f,
                //HandPosition = new Vector3(0.01f, -0.02f, 0.06f),
                PlayerHigh = 1.7f,
                CaptureFrameRate = 90,
                TouchRefreshRate = 120,
                CameraSmooth = 0.1f,
                HapticDuration = 0.1f,
                HapticAmplitude = 1
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
    void Update()
    {
        
    }
    
}
public class Settings
{
    public float HandSize { get; set; }
    //public Vector3 HandPosition { get; set; }
    public float PlayerHigh { get; set; }
    public int CaptureFrameRate { get; set; }
    public float TouchRefreshRate { get; set; }
    public float CameraSmooth { get; set; }
    public float HapticDuration { get; set; }
    public float HapticAmplitude { get; set; }
}
