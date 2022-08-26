using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Locker : MonoBehaviour
{
    public float Delay = 2.0f;
    float targetTime;
    public List<GameObject> buttons;
    public GameObject LocalMotion;
    public List<GameObject> RayObjects;
    bool isLocked = false;
    Material material;
    void Start()
    {
        targetTime = Delay;
        material = GetComponent<Renderer>().material;
        material.color = Color.green;
        Debug.Log("ButtonLock Start");
    }

    // Update is called once per frame
    void ToggleLocker()
    {
        ToggleButtons();
        ToggleLocalmotion();
        //ToggleRay();
        isLocked = !isLocked;
        if (isLocked)
            material.color = Color.red;
        else
            material.color = Color.green;
    }
    void OnTriggerStay(Collider other) 
    {
        targetTime -= Time.deltaTime;
        if (targetTime <= 0.0f)
        {
            ToggleLocker();
            targetTime = Delay;
        }
    }
    void ToggleLocalmotion()
    {
        LocalMotion.GetComponent<ContinuousMoveProviderBase>().enabled = !LocalMotion.GetComponent<ContinuousMoveProviderBase>().enabled;
        LocalMotion.GetComponent<ContinuousTurnProviderBase>().enabled = !LocalMotion.GetComponent<ContinuousTurnProviderBase>().enabled;
    }
    void ToggleButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
            buttons[i].SetActive(!buttons[i].activeSelf);
    }
    void ToggleRay()
    {
        for (int i = 0; i < RayObjects.Count; i++)
            RayObjects[i].GetComponent<RayManager>().RaySwitch= !RayObjects[i].GetComponent<RayManager>().RaySwitch;
    }
}
