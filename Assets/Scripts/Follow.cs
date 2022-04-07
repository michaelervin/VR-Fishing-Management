using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform followTransform;      // set in inspector
    public Vector3 offset;
    Transform cachedTrans;

    Rigidbody rb;

    void Start()
    {
        cachedTrans = transform;
        rb = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        if (rb == null)
        {
            cachedTrans.position = followTransform.position + offset;
        }
    }

    void FixedUpdate()
    {
        if(rb != null)
        {
            rb.isKinematic = true;
            rb.MovePosition(followTransform.position + offset);
        }
    }
}
