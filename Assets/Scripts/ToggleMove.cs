using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToggleMove : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (GetComponent<ContinuousMoveProviderBase>().enabled)
            {
                GetComponent<ContinuousMoveProviderBase>().enabled = false;
                GetComponent<ContinuousTurnProviderBase>().enabled = false;
            }  
            else
            {
                GetComponent<ContinuousMoveProviderBase>().enabled = true;
                GetComponent<ContinuousTurnProviderBase>().enabled = true;            
            }
        }
    }
}
