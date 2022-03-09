using UnityEngine;

public class TouchToSerial : MonoBehaviour
{
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
    }

}
