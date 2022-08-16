using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RayManager : MonoBehaviour
{
    public bool RaySwitch = true;
    XRRayInteractor interactor;
    XRInteractorLineVisual lineVisual;
    LineRenderer lineRenderer;
    void Start()
    {
        interactor = GetComponent<XRRayInteractor>();
        lineVisual = GetComponent<XRInteractorLineVisual>();
        lineRenderer = lineVisual.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.z > 0.2 || !RaySwitch)
        {
            interactor.enabled = false;
            lineRenderer.enabled = false;
            lineVisual.enabled = false;
        }
        else
        {
            interactor.enabled = true;
            lineRenderer.enabled = true;
            lineVisual.enabled = true;
        }
    }
}
