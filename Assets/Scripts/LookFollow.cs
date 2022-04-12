using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookFollow : MonoBehaviour
{
    [SerializeField] Transform other;
    [SerializeField] Vector3 rotationOffset;

    void Update()
    {
        transform.LookAt(other);
        transform.Rotate(rotationOffset);
    }
}
