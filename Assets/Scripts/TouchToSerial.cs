using UnityEngine;

public class TouchToSerial : MonoBehaviour
{
    public int Area;
    private int _insideColliderCount = 0;
    private void OnTriggerEnter(Collider other)
    {
        Serial.ChangeTouch((int)Area, true);
        Serial.SendTouch();
        _insideColliderCount += 1;
    }

    private void OnTriggerExit(Collider other)
    {
        if (_insideColliderCount == 0)
        {
            Serial.ChangeTouch((int)Area, false);
            Serial.SendTouch();
        }
        _insideColliderCount -= 1;
        _insideColliderCount = Mathf.Max(0, _insideColliderCount);
    }
}
