using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class ButtonToKey : MonoBehaviour
{
    [DllImport("user32.dll")]
    public static extern uint MapVirtualKey(uint uCode, uint uMapType);
    [DllImport("user32.dll")]
    static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
    //public enum VirtualKeyCode;
    public byte keyToPress;
    public Light lightTarget;
    public float frequency;
    public float amplitude;

    // Start is called before the first frame update
    void Start()
    {
        lightTarget.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        keybd_event(System.Convert.ToByte(keyToPress), (byte)MapVirtualKey((uint)keyToPress, 0), 0, UIntPtr.Zero);  
        lightTarget.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        keybd_event(System.Convert.ToByte(keyToPress), (byte)MapVirtualKey((uint)keyToPress, 0), 2, UIntPtr.Zero);
        lightTarget.gameObject.SetActive(false);
    }
    
}
