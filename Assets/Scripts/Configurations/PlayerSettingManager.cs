using UnityEngine.UI;
using UnityEngine;

public class PlayerSettingManager : MonoBehaviour
{
    public Transform LHandTransform;
    public Transform RHandTransform;
    public Transform PlayerTransform;
    private ValueManager PlayerHeightValue;
    private Slider Slider;
    void Start()
    {
        Slider = GetComponent<Slider>();
        PlayerHeightValue = GetComponent<ValueManager>();
        switch (gameObject.name)
        {
            case "PlayerHAdd":
            case "PlayerHSub":
                GetPlayerHeight();
                break;
            case "HandS":
                GetHandSize();
                break;
            case "HandX":
                GetHandPositionX();
                SetHandPositionX(Slider.value);
                break;
            case "HandY":
                GetHandPositionY();
                SetHandPositionY(Slider.value);
                break;
            case "HandZ":
                GetHandPositionZ();
                SetHandPositionZ(Slider.value);
                break;
        }
    }
    public void GetPlayerHeight()
    {
        if (JsonConfig.HasKey("PlayerHeight"))
            PlayerHeightValue.Value = (float)JsonConfig.GetDouble("PlayerHeight");
        SetPlayerHeight();
    }
    public void GetHandSize()
    {
        if (JsonConfig.HasKey("HandSize"))
            Slider.value = (float)JsonConfig.GetDouble("HandSize");
        SetHandSize(Slider.value);
    }
    public void GetHandPositionX()
    {
        if (JsonConfig.HasKey("HandPosition"))
            Slider.value = (float)JsonConfig.GetDouble("HandPositionX");
        SetHandPositionX(Slider.value);
    }
    public void GetHandPositionY()
    {
        if (JsonConfig.HasKey("HandPosition"))
            Slider.value = (float)JsonConfig.GetDouble("HandPositionY");
        SetHandPositionY(Slider.value);
    }
    public void GetHandPositionZ()
    {
        if (JsonConfig.HasKey("HandPosition"))
            Slider.value = (float)JsonConfig.GetDouble("HandPositionZ");
        SetHandPositionZ(Slider.value);
    }
    
    public void SetPlayerHeight()
    {
        PlayerTransform.position = new Vector3(PlayerTransform.position.x, PlayerHeightValue.Value, PlayerTransform.position.z);
        JsonConfig.SetDouble("PlayerHeight", PlayerHeightValue.Value);
    }
    public void SetHandSize(float value)
    {
        JsonConfig.SetDouble("HandSize", value);
        value = value / 100;
        LHandTransform.localScale = new Vector3(value, value, value);
        RHandTransform.localScale = new Vector3(value, value, value);
    }
    public void SetHandPositionX(float value)
    {
        JsonConfig.SetDouble("HandPositionX", value);
        value = value / 100;
        LHandTransform.localPosition = new Vector3(value, LHandTransform.localPosition.y, LHandTransform.localPosition.z);
        RHandTransform.localPosition = new Vector3(-value, RHandTransform.localPosition.y, RHandTransform.localPosition.z);
    }
    public void SetHandPositionY(float value)
    {
        JsonConfig.SetDouble("HandPositionY", value);
        value = value / 100;
        LHandTransform.localPosition = new Vector3(LHandTransform.localPosition.x, value, LHandTransform.localPosition.z);
        RHandTransform.localPosition = new Vector3(RHandTransform.localPosition.x, value, RHandTransform.localPosition.z);
    }
    public void SetHandPositionZ(float value)
    {
        JsonConfig.SetDouble("HandPositionZ", value);
        value = value / 100;
        LHandTransform.localPosition = new Vector3(LHandTransform.localPosition.x, LHandTransform.localPosition.y, value);
        RHandTransform.localPosition = new Vector3(RHandTransform.localPosition.x, RHandTransform.localPosition.y, value);
    }
}
