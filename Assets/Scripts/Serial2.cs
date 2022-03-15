using UnityEngine;
using System.IO.Ports;
using System;
public class Serial2 : MonoBehaviour
{
    static SerialPort p1Serial = new SerialPort ("COM6", 9600);
    int packleng = 0;
    byte[] incomPacket = new byte[6];
    byte[] settingPacket = new byte[6];
    static byte[] touchPacket = new byte[9];
    float timer = 0; 
    bool failed = false;
    byte recivData;
    void Start()
    {
        settingPacket[0] = 40;
        settingPacket[5] = 41;
        touchPacket[0] = 40;
        touchPacket[8] = 41;
        Debug.Log("Start Serial2");
        p1Serial.Open();
        Debug.Log("Serial2 Started");
    }

    void Update()
    {
        ReadPack();
        if (!failed)
            TouchSetUp(); 
    }

    private void TouchSetUp()
    {
        switch (incomPacket[3])
        {
            case 76:
            case 69:
                break;
            case 114:
            case 107:
                for (int i=1; i<5; i++)
                    settingPacket[i] = incomPacket[i];    
                p1Serial.Write(settingPacket, 0, settingPacket.Length);
                Array.Clear(incomPacket, 0, incomPacket.Length);
                break;
            case 65:
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
}