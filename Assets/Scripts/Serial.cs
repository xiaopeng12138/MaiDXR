using System.Collections;
using UnityEngine;
using System.IO.Ports;
using System;
public class Serial : MonoBehaviour
{
    static SerialPort p1Serial = new SerialPort ("COM5", 9600);
    int packleng = 0;
    byte[] incomPacket = new byte[6];
    byte[] settingPacket = new byte[6];
    static byte[] touchPacket = new byte[9];
    static byte[] touchPacketReset = new byte[9];
    
    static bool startUp = true; //use ture for default start up state to prevent restart game
    float timer = 0; 
    bool failed = false;
    byte recivData;
    
    void Start()
    {
        SerialStartUp();
    }

    void Update()
    {
        ReadPack();
        if (!failed)
            TouchSetUp();
        //SendTouch();     
    }

    private void SerialStartUp()
    {
        settingPacket[0] = 40;
        settingPacket[5] = 41;
        touchPacket[0] = 40;
        touchPacket[8] = 41;
        touchPacketReset[0] = 40;
        touchPacketReset[8] = 41;
        p1Serial.Open();
        Debug.Log("Serial Started");
    }

    private void TouchSetUp()
    {
        switch (incomPacket[3])
        {
            case 76:
            case 69:
                startUp = false;
                break;
            case 114:
            case 107:
                for (int i=1; i<5; i++)
                    settingPacket[i] = incomPacket[i];    
                p1Serial.Write(settingPacket, 0, settingPacket.Length);
                Array.Clear(incomPacket, 0, incomPacket.Length);
                break;
            case 65:
                startUp = true;
                break;
        }
    }

    private void ReadPack()
    {
        timer = 0f;
        if (p1Serial.BytesToRead == 6)
        {
            packleng = 0;
            while (packleng < 6) 
            {
                recivData = Convert.ToByte(p1Serial.ReadByte());
                if (recivData == 123) 
                {
                    packleng = 0;
                }
                incomPacket[packleng++] = recivData;
                if(timer > 20f){ failed = true; break; }
                timer += Time.deltaTime;
            }
        }
    }

    public static void SendTouch()
    {
        if (startUp)
            p1Serial.Write(touchPacket, 0, 9);
    }

    public static void ChangeTouch(int Area, bool State)
    {
        if (startUp)
            ByteArrayExt.SetBit(touchPacket, Area+8, State);
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