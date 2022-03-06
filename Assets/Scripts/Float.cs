using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Float : MonoBehaviour
{
    [SerializeField] float buoyancyForce = 9.8f;

    Rigidbody rb;
    bool inWater;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inWater = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            rb.AddForce(Vector3.up * buoyancyForce);
        }
    }
}
