using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("Select Player")]
    public Transform target;
    [Tooltip("Default: 0, 0.04, -1")]
    public Vector3 offset;
    [Tooltip("Default: 0.05")]
    public float damping;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, damping) + offset;
    }
}
