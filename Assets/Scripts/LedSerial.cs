
using UnityEngine;
using System.IO.Ports;
using System;
using System.Collections;

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
    void Start()
    {
        p1Serial.Open();
        Debug.Log("LED Serial Started");
    }
    void Update()
    {
        //ReadPack();
        ReadData();
        FixLedPower();
    }
    void FixLedPower()
    {
        for (int i = 0; i < 8; i++)
        {
            Lights[i].intensity = LightIntensity / 2 * ((Lights[i].color.r + Lights[i].color.g + Lights[i].color.b)/3);
        }
    }
    private void ReadPack()
    {
        ReadData();
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
                if (dataPacket[6] > 8)
                    dataPacket[6] = 8;
                for (int i = dataPacket[5]; i < dataPacket[6]; i++)
                {
                    Lights[i].color = new Color32(dataPacket[8], dataPacket[9], dataPacket[10], 255);
                }
                PrevFadeColor = new Color32(dataPacket[8], dataPacket[9], dataPacket[10], 255);
                break;
            case 51:
                if (dataPacket[6] > 8)
                    dataPacket[6] = 8;
                Color32 nowCorlor = new Color32(dataPacket[8], dataPacket[9], dataPacket[10], 255);
                StartCoroutine(Fade(dataPacket[5], dataPacket[6], Lights,PrevFadeColor, nowCorlor, dataPacket[11]));
                PrevFadeColor = new Color32(dataPacket[8], dataPacket[9], dataPacket[10], 255);
                //Debug.Log("Fade");
                break;
        }
    }
    private IEnumerator Fade(byte start, byte end, Light[] Lights, Color32 prevColor, Color32 nowColor, float duration)
    {
        duration = 4095 / duration * 8 / 1000;
        //Debug.Log(duration);
        for (float time = 0f; time < duration; time += Time.deltaTime)
        {
            float progress = time / duration;
            for (int i = start; i < end; i++)
            {
                Lights[i].color = Color.Lerp(prevColor, nowColor, progress);
            }
            yield return null;
        }
    }
}
