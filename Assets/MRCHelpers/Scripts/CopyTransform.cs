using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MRCHelpers
{

    /// <summary>
    /// Copies the position of a transform every frame
    /// </summary>
    public class CopyTransform : MonoBehaviour
    {
        /// <summary>
        /// The transform to follow
        /// </summary>
        [SerializeField]
        private Transform m_originalTransform;

        /// <summary>
        /// Update
        /// </summary>
        void Update()
        {
            //copy the pose of the other transform each frame
            transform.position = m_originalTransform.position;
            transform.rotation = m_originalTransform.rotation;
        }
    }

}