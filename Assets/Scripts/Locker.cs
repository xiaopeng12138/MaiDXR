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
        for (int i = 0; i < buttons.Count; i++)
            buttons[i].SetActive(!buttons[i].activeSelf);
        
        LocalMotion.GetComponent<ContinuousMoveProviderBase>().enabled = !LocalMotion.GetComponent<ContinuousMoveProviderBase>().enabled;
        LocalMotion.GetComponent<ContinuousTurnProviderBase>().enabled = !LocalMotion.GetComponent<ContinuousTurnProviderBase>().enabled;
        
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
}
