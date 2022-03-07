using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public FishData data;

    [HideInInspector]
    public FishContainer container;

    Rigidbody rb;
    Boid boid;
    Hook attatchedHook;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        boid = GetComponent<Boid>();
    }

    private void FixedUpdate()
    {
        if (attatchedHook != null)
        {
            rb.MovePosition(attatchedHook.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        FishFood fishFood = other.GetComponent<FishFood>();
        if(fishFood != null)
        {
            Eat(fishFood);
        }
    }

    public void AttachHook(Hook hook)
    {
        attatchedHook = hook;
        if (boid != null)
        {
            boid.enabled = false;
        }
    }

    private void Eat(FishFood fishFood)
    {
        Destroy(fishFood.gameObject);
        Debug.Log("Nom");
    }
}
