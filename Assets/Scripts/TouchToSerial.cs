using System;
using System.Runtime.InteropServices;
using System.Collections;
using UnityEngine;

public class TouchToSerial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Serial.SendTouch();
    }

    public int Area;
    private void OnTriggerEnter(Collider other)
    {
        Serial.ChangeTouch((int)Area, true);
        Serial.SendTouch();
    }

    private void OnTriggerExit(Collider other)
    {
        Serial.ChangeTouch((int)Area, false);
        Serial.SendTouch();
        //Serial.ResetTouch();
    }

}
