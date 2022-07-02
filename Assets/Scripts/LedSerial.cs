
using UnityEngine;
using System.IO.Ports;
using System;
using System.Collections;
using System.Collections.Generic;

public class LedSerial : MonoBehaviour
{
    static SerialPort p1Serial = new SerialPort ("COM51", 115200);
    // Start is called before the first frame update
    List<byte> dataPacket = new List<byte>();
    List<byte> incomPacket = new List<byte>();
    byte recivData;
    public Light[] Lights;
    public Light BodyLight;
    public Light DisplayLight;
    Color32 PrevFadeColor;
    Color32 nowCorlor;
    void Start()
    {
        Debug.Log("Started LED Serial");
        try
        {
            p1Serial.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to Open Serial Ports: {ex}");
        }
        Debug.Log("LED Serial Started");
    }
    void Update()
    {
        if (p1Serial.IsOpen)
            ReadData();
        UpdateLED();
    }
    void OnDestroy()
    {
        p1Serial.Close();
    }
    void ReadData()
    {
        while (p1Serial.BytesToRead > 0)
        {
            recivData = Convert.ToByte(p1Serial.ReadByte());
            if (recivData == 224)
            {
                dataPacket = new List<byte>(incomPacket);
                incomPacket.Clear();
                incomPacket.Add(recivData);
                return;
            }
            incomPacket.Add(recivData);
        }    
    }

    void UpdateLED()
    {
        if (dataPacket.Count < 8)
            return;
        switch (dataPacket[4])
        { 
            case 49:
                if (dataPacket[5] > 7)
                    dataPacket[5] = 7;
                Lights[dataPacket[5]].color = new Color32(dataPacket[6], dataPacket[7], dataPacket[8], 255);
                dataPacket.Clear();
                break;
            case 50:
            case 51:
                if (dataPacket[6] > 8)
                    dataPacket[6] = 8;
                nowCorlor = new Color32(dataPacket[8], dataPacket[9], dataPacket[10], 255);
                if (dataPacket[4]==50)
                    Switch(dataPacket[5], dataPacket[6], Lights, nowCorlor);
                else
                    StartCoroutine(Fade(dataPacket[5], dataPacket[6], Lights,PrevFadeColor, nowCorlor, dataPacket[11]));
                PrevFadeColor = new Color32(dataPacket[8], dataPacket[9], dataPacket[10], 255);
                dataPacket.Clear();
                break;
            case 57:
                BodyLight.color = new Color32(dataPacket[5], dataPacket[5], dataPacket[5], 255);
                DisplayLight.color = new Color32(dataPacket[6], dataPacket[6], dataPacket[6], 255);
                dataPacket.Clear();
                break;
        }
    }
    IEnumerator Fade(byte start, byte end, Light[] Lights, Color32 prevColor, Color32 nowColor, float duration)
    {
        duration = 4095 / duration * 8 / 1000;
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
    void Switch(byte start, byte end, Light[] Lights, Color32 Color)
    {
        for (int i = start; i < end; i++)
        {
            Lights[i].color = Color;
        }
    }
}
