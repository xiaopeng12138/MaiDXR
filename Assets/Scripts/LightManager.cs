
using UnityEngine;
using System.IO.Ports;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class LightManager : MonoBehaviour
{
    static SerialPort p1Serial = new SerialPort ("COM51", 115200);
    public List<List<byte>> dataListP1 = new List<List<byte>>();
    public List<List<byte>> dataListStreamP1 = new List<List<byte>>();
    public List<List<byte>> dataListInstantP1 = new List<List<byte>>();
    static bool isUpdateCMD = false;
    public List<Light> RingLeds = new List<Light>();
    public Light BodyLed;
    public Light DisplayLed;
    public float BodyLedIntensity = 0.0f;
    public float DisplayLedIntensity = 0.0f;
    Color32 PrevFadeColor;
    Color32 nowCorlor;
    Thread thread;
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
        thread = new Thread(new ParameterizedThreadStart(ReadDataList));
        thread.Start(p1Serial);
    }
    void Update()
    {
        if (isUpdateCMD)
        {
            UpdateLedListInstant(ref dataListInstantP1, RingLeds, BodyLed, DisplayLed);
            isUpdateCMD = false;
        }
        UpdateLedListStream(ref dataListStreamP1, RingLeds, BodyLed, DisplayLed);
    }
    void OnDestroy()
    {
        thread.Abort();
        p1Serial.Close();
    }

    void ReadDataList(object Serial)
    {
        SerialPort _serial = (SerialPort)Serial;

        byte headbyte = 0;
        List<byte> data = new List<byte>();
        List<List<byte>> dataList = new List<List<byte>>();

        while (true)
        {
            if (!_serial.IsOpen && _serial.BytesToRead < 1)
                continue;
            else if (!isUpdateCMD)
                headbyte = (byte)_serial.ReadByte();
            if (headbyte == 224)
            {
                data = ReadData(_serial);
                SeperatData(data);
                headbyte = 0;
                //Debug.Log($"data: {string.Join(", ", data)}");
            }
        }
    }

    void SeperatData(List<byte> data)
    {
        if (data[0] == 57)
            dataListStreamP1.Add(data);
        else if (data[0] == 60)
            isUpdateCMD = true;

        if (!isUpdateCMD)
            dataListInstantP1.Add(data);
    }

    List<byte> ReadData(SerialPort Serial)
    {
        List<byte> data = new List<byte>();
        byte[] head = new byte[3];
        Serial.Read(head, 0, 3);
        for (int i = 0; i < head[2]; i++) 
            data.Add((byte)Serial.ReadByte());
        return data;
    }

    void UpdateLedListStream(ref List<List<byte>> dataList, List<Light> ringLeds, Light bodyLed, Light displayLed)
    {
        if (dataList.Count < 1)
            return;
        UpdateLED(dataList[0], ringLeds, bodyLed, displayLed);
        dataList.RemoveAt(0);
        if (dataList.Count > 64)
            dataList.Clear();
    }

    void UpdateLedListInstant(ref List<List<byte>> dataList, List<Light> ringLeds, Light bodyLed, Light displayLed)
    {
        while (dataList.Count > 0)
        {
            UpdateLED(dataList[0], ringLeds, bodyLed, displayLed);
            dataList.RemoveAt(0);
        }
    }

    void UpdateLED(List<byte> _data, List<Light> ringLeds, Light bodyLed, Light dispayLed)
    {
        var data = new List<byte>(_data);
        byte mp;
        if (data.Count < 3)
            return;
        switch (data[0])
        { 
            case 49:
                //Debug.Log($"CMD49: {string.Join(", ", data)}");
                int index = data[1];
                mp = Convert.ToByte(127 * ((data[2]+data[3]+data[4]) / 765));
                ringLeds[index].color = new Color32((byte)(data[2] - mp), (byte)(data[3] - mp), (byte)(data[4] - mp), 255);
                if (!SerialManager.startUp)
                    SerialManager.startUp = true;
                break;
            case 50:
            case 51:
                //Debug.Log($"CMD50/51: {string.Join(", ", data)}");
                if (data[2] > 8)
                    data[2] = 8;
                mp = Convert.ToByte(127 * ((data[4]+data[5]+data[6]) / 765));
                nowCorlor = new Color32((byte)(data[4] - mp), (byte)(data[5] - mp), (byte)(data[6] - mp), 255);
                if (data[0]==50)
                    Switch(data[1], data[2], ringLeds, nowCorlor);
                else
                {
                    StopCoroutine(Fade(data[1], data[2], ringLeds, PrevFadeColor, nowCorlor, data[7]));
                    StartCoroutine(Fade(data[1], data[2], ringLeds, PrevFadeColor, nowCorlor, data[7]));
                }
                PrevFadeColor = nowCorlor;
                break;
            case 57:
                //Debug.Log($"CMD57: {string.Join(", ", data)}");
                bodyLed.intensity = BodyLedIntensity * (data[1] / 255f);
                dispayLed.intensity = DisplayLedIntensity * (data[2] / 255f);
                break;
        }
    }
    IEnumerator Fade(byte start, byte end, List<Light> ringLeds, Color32 prevColor, Color32 nowColor, float duration)
    {
        duration = 4095 / duration * 8 / 1000;
        for (float time = 0f; time < duration; time += Time.deltaTime)
        {
            float progress = time / duration;
            for (int i = start; i < end; i++)
            {
                ringLeds[i].color = Color.Lerp(prevColor, nowColor, progress);
            }
            yield return null;
        }
    }
    void Switch(byte start, byte end, List<Light> ringLeds, Color32 Color)
    {
        for (int i = start; i < end; i++)
        {
            ringLeds[i].color = Color;
        }
    }
}