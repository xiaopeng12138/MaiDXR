using UnityEngine;
using System;
public class TouchPanelManager : MonoBehaviour
{
    int Area;
    private int _insideColliderCount = 0;
    public static event Action touchDidChange;
    void Start()
    {
        Area = (int)Enum.Parse(typeof(TouchArea), gameObject.name);
    }
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
    enum TouchArea
    {
        A1 = 0, A2 = 1, A3 = 2, A4 = 3, A5 = 4, 
        A6 = 8, A7 = 9, A8 = 10, B1 = 11, B2 = 12, 
        B3 = 16, B4 = 17, B5 = 18, B6 = 19, B7 = 20, 
        B8 = 24, C1 = 25, C2 = 26, D1 = 27, D2 = 28, 
        D3 = 32, D4 = 33, D5 = 34, D6 = 35, D7 = 36, 
        D8 = 40, E1 = 41, E2 = 42, E3 = 43, E4 = 44, 
        E5 = 48, E6 = 49, E7 = 50, E8 = 51,
    }
}
