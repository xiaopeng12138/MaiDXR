using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    public GameObject Tab1Object;
    public GameObject Tab2Object;
    void Start()
    {
        OnTab1Click();
    }
    public void OnTab1Click()
    {
        Tab1Object.SetActive(true);
        Tab2Object.SetActive(false);
    }
    public void OnTab2Click()
    {
        Tab1Object.SetActive(false);
        Tab2Object.SetActive(true);
    }
}
