using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenIdleManager : MonoBehaviour
{
    public Texture IdleTexture;
    public float TimeOut;
    private Texture OldTexture;
    private float timeCounter;
    private Material Material;
    void Start()
    {
        Material = GetComponent<Renderer>().material;
    }
    void Update()
    {
        timeCounter += Time.deltaTime;
        if (OldTexture != Material.mainTexture || Material.mainTexture != null)
        {
            timeCounter = 0;
            OldTexture = Material.mainTexture;
        }
        if (timeCounter > TimeOut)
        {
            Material.mainTexture = IdleTexture;
            timeCounter = 0;
        }
    }
}
