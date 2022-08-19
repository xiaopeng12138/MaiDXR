using UnityEngine;
using System.IO.Ports;
using System;
using System.Collections;
using System.Threading;
public class SerialManager : MonoBehaviour
{
    static SerialPort p1Serial = new SerialPort ("COM5", 9600);
    static SerialPort p2Serial = new SerialPort ("COM6", 9600);
    static byte[] settingPacket = new byte[6] {40, 0, 0, 0, 0, 41};
    static byte[] touchData = new byte[9] {40, 0, 0, 0, 0, 0, 0, 0, 41};
    static byte[] touchData2 = new byte[9] {40, 0, 0, 0, 0, 0, 0, 0, 41};
    public static bool startUp = false; //use ture for default start up state to prevent restart game
    static string recivData;
    private Thread touchThread;
    private Queue touchQueue;
    
    void Start()
    {
        try
        {
            Debug.Log("Try start Serial");
            p1Serial.Open();
            p2Serial.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to Open Serial Ports: {ex}");
        }
        touchQueue = Queue.Synchronized(new Queue());
        touchThread = new Thread(TouchThread);
        InvokeRepeating("PingTouchThread", 0, 1);
        TouchPanelManager.touchDidChange += PingTouchThread;
        touchThread.Start();
        Debug.Log("Serial Started");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            startUp = !startUp;
    }
    private void PingTouchThread()
    {
        touchQueue.Enqueue(1);
    }
    private void TouchThread()
    {
        while(true)
        {
            if(p1Serial.IsOpen)
                ReadData(p1Serial);
            if(p2Serial.IsOpen)
                ReadData(p2Serial);
            if(touchQueue.Count > 0)
            {
                touchQueue.Dequeue();
                UpdateTouch();
            }
        }
    }
    private void OnDestroy()
    {
        touchThread.Abort();
        p1Serial.Close();
        p2Serial.Close();
    }

    private void ReadData(SerialPort Serial)
    {
        if (Serial.BytesToRead == 6)
        {
            recivData = Serial.ReadExisting();
            TouchSetUp(Serial, recivData); 
        }
    }
    private void TouchSetUp(SerialPort Serial, string data)
    {
        switch (Convert.ToByte(data[3]))
        {
            case 76:
            case 69:
                startUp = false;
                break;
            case 114:
            case 107:
                for (int i=1; i<5; i++)
                    settingPacket[i] = Convert.ToByte(data[i]);    
                Serial.Write(settingPacket, 0, settingPacket.Length);
                break;
            case 65:
                startUp = true;
                break;
        }
    }

    public static void SendTouch(SerialPort Serial, byte[] data)
    {
        if (startUp)
            Serial.Write(data, 0, 9);
    }
    public static void UpdateTouch()
    {
        if (!startUp)
            return;
        SendTouch(p1Serial, touchData);
        SendTouch(p2Serial, touchData2);
    }

    public static void ChangeTouch(bool isP1, int Area, bool State)
    {
        if (isP1)
            ByteArrayExt.SetBit(touchData, Area+8, State);
        else
            ByteArrayExt.SetBit(touchData2, Area+8, State);
    }
}

public static class ByteArrayExt
{
    public static byte[] SetBit(this byte[] self, int index, bool value)
    { 
        var bitArray = new BitArray(self);
        bitArray.Set(index, value);
        bitArray.CopyTo(self, 0);
        return self;
    }
}