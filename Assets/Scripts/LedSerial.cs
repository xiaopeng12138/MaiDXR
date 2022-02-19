using UnityEngine;
using System.IO.Ports;
using System;

public class LedSerial : MonoBehaviour
{
    static SerialPort p1Serial = new SerialPort ("COM51", 115200);
    // Start is called before the first frame update
    byte[] dataPacket = new byte[13];
    byte[] incomPacket = new byte[13];
    float timer = 0; 
    byte recivData;
    int packLeng = 0;
    public Light[] Lights;
    public float LightIntensity = 1;
    Color32 PrevFadeColor;
    bool headState = false;
    float t;
    void Start()
    {
        p1Serial.Open();
        Debug.Log("LED Serial Started");

    }
    void Update()
    {
        //ReadPack();
        ReadData();
        //FixLedPower();
        //UpdatePacks();
    }
    void FixLedPower()
    {
        for (int i = 0; i < 8; i++)
        {
            Lights[i].intensity = LightIntensity / (Lights[i].color.r + Lights[i].color.g + Lights[i].color.b);
        }
    }
    private void ReadPack()
    {
        ReadData();
        //StartCoroutine(ReadData());
        Debug.Log("RX: "+dataPacket[0]+"-"+
                                    dataPacket[1]+"-"+
                                    dataPacket[2]+"-"+
                                    dataPacket[3]+"-"+
                                    dataPacket[4]+"-"+
                                    dataPacket[5]+"-"+
                                    dataPacket[6]+"-"+
                                    dataPacket[7]+"-"+
                                    dataPacket[8]+"-"+
                                    dataPacket[9]+"-"+
                                    dataPacket[10]);
    }
    private void ReadData()
    {
        timer = 0f;
        if (p1Serial.BytesToRead >= 3)
        {
            if (!headState)
                recivData = Convert.ToByte(p1Serial.ReadByte());
            headState = false;
            if (recivData == 224) 
            {
                do
                {
                        incomPacket[packLeng++] = recivData;
                        recivData = Convert.ToByte(p1Serial.ReadByte());
                }
                while (recivData != 224 & p1Serial.BytesToRead >= 1);
                headState = true;
                dataPacket = incomPacket;
                packLeng = 0;
                UpdateLED();
            }
        }
    }

    private void UpdateLED()
    {
        switch (dataPacket[4])
        { 
            case 49:
                if (dataPacket[5] > 7)
                    dataPacket[5] = 7;
                Lights[dataPacket[5]].color = new Color32(dataPacket[6], dataPacket[7], dataPacket[8], 255);
                break;
            case 50:
            case 51:
                if (dataPacket[6] > 8)
                    dataPacket[6] = 8;
                for (int i = dataPacket[5]; i < dataPacket[6]; i++)
                {
                    Lights[i].color = new Color32(dataPacket[8], dataPacket[9], dataPacket[10], 255);
                }
                //PrevFadeColor = new Color32(dataPacket[8], dataPacket[9], dataPacket[10], 255);
                break;
            /*
            case 51:
                if (dataPacket[6] > 8)
                    dataPacket[6] = 8;
                    for (int i = dataPacket[5]; i < dataPacket[6]; i++)
                    {
                        Lights[i].color = Color.Lerp(PrevFadeColor, new Color32(dataPacket[8], dataPacket[9], dataPacket[10], 255), t);
                    }
                    if (t < 1)
                    { 
                        t += Time.deltaTime/1.5f;
                    }
                PrevFadeColor = new Color32(dataPacket[8], dataPacket[9], dataPacket[10], 255);
                break;
            */
        }
    }
}