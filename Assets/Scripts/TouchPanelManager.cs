using UnityEngine;
using System;
public class TouchPanelManager : MonoBehaviour
{
    public int Area;
    private int _insideColliderCount = 0;
    public static event Action touchDidChange;
    private void OnTriggerEnter(Collider other)
    {
        _insideColliderCount += 1;
        SerialManager.ChangeTouch(true, (int)Area, true);
        touchDidChange?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        _insideColliderCount -= 1;
        _insideColliderCount = Mathf.Max(0, _insideColliderCount);
        if (_insideColliderCount == 0)
        {
            SerialManager.ChangeTouch(true, (int)Area, false);
            touchDidChange?.Invoke();
        }
    }
}
