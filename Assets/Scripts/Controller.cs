using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class Controller : MonoBehaviour
{
    public XRNode Hand;
    InputDevice device;
    public float duration;
    public float amplitude;
    private void OnTriggerEnter(Collider other)
    {
        device = InputDevices.GetDeviceAtXRNode(Hand);
        device.SendHapticImpulse(0, amplitude, duration);
    }
    private void OnTriggerExit(Collider other)
    {
        device = InputDevices.GetDeviceAtXRNode(Hand);
        device.StopHaptics();
    }
}
