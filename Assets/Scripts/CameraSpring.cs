using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpring : MonoBehaviour
{
    public Transform target;

    private Vector3 delta;

    // Use this for initialization
    void Start()
    {
        delta = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + delta;
    }
}
