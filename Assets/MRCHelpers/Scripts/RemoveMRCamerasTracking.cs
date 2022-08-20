using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;

namespace MRCHelpers
{

    /// <summary>
    /// Removes the tracked pose driver in the cameras used for mixed reality capture.
    /// Unity automatically adds it, and this way, the cameras follow the head of the 
    /// user, while they should be fixed in the world
    /// </summary>
    public class RemoveMRCamerasTracking : MonoBehaviour
    {
        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            StartCoroutine(RemoveCamerasTracking());
        }

        /// <summary>
        /// Coroutine to remove tracking scripts from cameras
        /// </summary>
        private IEnumerator RemoveCamerasTracking()
        {
            //get the tracking space child
            Transform trackingSpaceTransform = transform.Find("TrackingSpace");

            //the names of the cameras that Oculus adds for MRC
            string[] camerasNames = new string[] { "OculusMRC_BackgroundCamera", "OculusMRC_ForgroundCamera" };
            Transform tr = null;

            //let everything initialize
            yield return null;
            var waiter = new WaitForSeconds(0.5f);

            //I have noticed the system may re-created the cameras in some conditions
            //(e.g. the player disconnects and reconnects with OBS), so we must keep deleting them forever
            while (true)
            {
                //for each camera
                foreach (string cameraName in camerasNames)
                {
                    //find the camera, and destroy its TrackedPoseDriver if there is one attached
                    if ((tr = trackingSpaceTransform.Find(cameraName)) != null)
                        if (tr.GetComponent<TrackedPoseDriver>() != null)
                            Destroy(tr.GetComponent<TrackedPoseDriver>());
                }

                //wait a bit before looking again
                yield return waiter;
            }
        }
    }

}