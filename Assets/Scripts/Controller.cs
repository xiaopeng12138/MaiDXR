using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public InputDeviceRole Hand;
    //InputDevice device;
    public float duration;
    public float amplitude;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        SendHapticOn(Hand, amplitude, duration);
    }

    private void OnTriggerExit(Collider other)
    {
        SendHapticOn(Hand, amplitude, duration);
        SendHapticOff(Hand);
    }
    
    void SendHapticOn(InputDeviceRole Hand, float amplitude, float duration)
    {
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>(); 
        InputDevices.GetDevicesWithRole(Hand, devices);
        uint channel = 0;
        foreach (var device in devices)
        {
            HapticCapabilities capabilities;
            if (device.TryGetHapticCapabilities(out capabilities))
            {
                if (capabilities.supportsImpulse)
                {  
                    device.SendHapticImpulse(channel, amplitude, duration);
                }
            }
        }
    }
    void SendHapticOff(InputDeviceRole Hand)
    {
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>(); 
        InputDevices.GetDevicesWithRole(Hand, devices);
        foreach (var device in devices)
        {
            HapticCapabilities capabilities;
            if (device.TryGetHapticCapabilities(out capabilities))
            {
                if (capabilities.supportsImpulse)
                    device.StopHaptics();
            }
        }
    }
}
