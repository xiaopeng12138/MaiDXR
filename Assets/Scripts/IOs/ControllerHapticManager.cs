using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class ControllerHapticManager : MonoBehaviour
{
    public XRNode Hand;
    InputDevice device;
    public float duration = 0.1f;
    public float amplitude = 1f;
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
    void Uppdate()
    {

    }
}
