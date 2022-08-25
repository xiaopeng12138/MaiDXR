using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonConfigBehavior : MonoBehaviour
{
    public static JsonConfigBehavior instance;
    void Awake() 
    {
        instance = this;
    }
    public static void saveFile()
    {
        instance.StopCoroutine(JsonConfig.saveFileWait());
        instance.StartCoroutine(JsonConfig.saveFileWait());
    }
}
