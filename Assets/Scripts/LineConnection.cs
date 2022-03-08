using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Draws a line from the object to another object
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class LineConnection : MonoBehaviour
{
    [SerializeField] Transform other;
    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, other.position);
    }
}
