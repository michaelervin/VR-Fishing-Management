using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookFollow : MonoBehaviour
{
    [SerializeField] Transform other;
    [SerializeField] Vector3 rotationOffset;
    [SerializeField] bool lockX;

    private float startX;

    private void Start()
    {
        startX = transform.rotation.eulerAngles.x;
    }

    void Update()
    {
        transform.LookAt(other);
        transform.Rotate(rotationOffset);
        if (lockX)
        {
            transform.Rotate(startX - transform.rotation.eulerAngles.x, 0, 0);
        }
    }
}
