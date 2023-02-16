using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    private Vector3 followOffset;
    private Vector3 desiredPos;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        followOffset = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        desiredPos = target.position + followOffset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothTime);
    }
}
