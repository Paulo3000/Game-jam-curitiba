using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public Transform target;
    //public float smoothTime = 0.3F;
    //private Vector3 velocity = Vector3.zero;
    //public Vector3 offset;

    //private void Start()
    //{
    //    offset = transform.position - target.position;
    //}

    //void Update()
    //{
    //    Vector3 targetPosition = target.TransformPoint(target.position + offset);

    //    transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    //}

    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
