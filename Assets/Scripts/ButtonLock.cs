using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLock : MonoBehaviour
{
    public List<GameObject> buttons;
    bool isLocked = false;
    Material material;
    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.color = Color.green;
        Debug.Log("ButtonLock Start");
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetActive(!buttons[i].activeSelf);
        }
        isLocked = !isLocked;
        if (isLocked)
        {
            material.color = Color.red;
        }
        else
        {
            material.color = Color.green;
        }
    }
}
