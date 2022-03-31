using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityTracker : MonoBehaviour
{
    public Vector3 velocity;
    Vector3 previousPos;

    void Start()
    {
        previousPos = transform.position;
    }

    void Update()
    {
        velocity = (transform.position - previousPos) / Time.deltaTime;
        previousPos = transform.position;
    }
}
