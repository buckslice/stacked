using UnityEngine;
using System.Collections;

namespace DerekEdrich
{
    [RequireComponent(typeof(Camera))]
    public class DepthBuffer : MonoBehaviour
    {

        // Use this for initialization
        void Awake()
        {
            GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
            this.enabled = false;
        }
    }
}