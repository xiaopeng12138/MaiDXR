using UnityEngine;

public class TouchToSerial : MonoBehaviour
{
    public int Area;
    int _insideColliderCount = 0;

    private void Update()
    {
        if (_insideColliderCount == 0)
        {
            Serial.ChangeTouch((int)Area, true);
            Serial.SendTouch();
        }
        else
        {
            Serial.ChangeTouch((int)Area, false);
            Serial.SendTouch();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        _insideColliderCount += 1;
    }

    private void OnTriggerExit(Collider other)
    {
        _insideColliderCount -= 1;
        _insideColliderCount = Mathf.Max(0, _insideColliderCount);
    }
}
