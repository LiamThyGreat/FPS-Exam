using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAtScript : MonoBehaviour
{
    private Transform lookAtTransform;

    private void Start()
    {
        lookAtTransform = FindObjectOfType<PlayerHealth>().transform;
    }

    void Update()
    {
        //Make the canvas face transform
        transform.LookAt(lookAtTransform);
    }
}
