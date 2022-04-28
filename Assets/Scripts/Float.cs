using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Float : MonoBehaviour
{
    [SerializeField] float buoyancyForce = 20f;
    [SerializeField] float waterDrag = 15;
    [SerializeField] float airDrag = 1;
    public AudioClip bloop;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = airDrag;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            rb.AddForce(Vector3.up * buoyancyForce);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            rb.drag = waterDrag;
            if(rb.velocity.y < -5)
            {
                AudioSource.PlayClipAtPoint(bloop, rb.transform.position);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            rb.drag = airDrag;
        }
    }
}
