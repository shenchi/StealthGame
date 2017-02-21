using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpring : MonoBehaviour
{
    public Transform target;

    public float initialYaw = 45.0f;
    public float initialPitch = 45.0f;
    public float minPitch = 30.0f;
    public float maxPitch = 60.0f;

    public float initialDistance = 10.0f;
    public float minDistance = 10.0f;
    public float maxDistance = 20.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float distance = 0.0f;

    private Vector3 delta;

    // Use this for initialization
    void Start()
    {
        yaw = initialYaw;
        pitch = initialPitch;
        distance = initialDistance;
    }


    Vector3 lastMousePos;
    // Update is called once per frame
    void Update()
    {
        var d = Input.GetAxis("Mouse ScrollWheel") * 10;
        distance = Mathf.Clamp(distance - d, minDistance, maxDistance);

        const int moveCameraButton = 1;

        if (Input.GetMouseButtonDown(moveCameraButton))
        {
            lastMousePos = Input.mousePosition;
        }
        //else if (Input.GetMouseButtonUp(moveCameraButton))
        //{

        //}
        else if (Input.GetMouseButton(moveCameraButton))
        {
            var mouseDelta = (Input.mousePosition - lastMousePos) * 0.5f;
            lastMousePos = Input.mousePosition;

            yaw = Mathf.Repeat(yaw + mouseDelta.x, 360.0f);
            pitch = Mathf.Clamp(pitch - mouseDelta.y, minPitch, maxPitch);
        }

        delta = Quaternion.Euler(pitch, yaw, 0.0f) * Vector3.back * distance;
        transform.position = target.position + delta;
        transform.LookAt(target);
    }
}
