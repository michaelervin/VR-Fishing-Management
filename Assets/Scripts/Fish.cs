using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public FishData data;

    public FishContainer container;

    /// <summary>
    /// The boid script attatched to the fish. This is null if the fish is not in a boid managed environment.
    /// </summary>
    Boid boid;
    Rigidbody rb;
    Hook attatchedHook;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        boid = GetComponent<Boid>();
    }

    public Boid AddBoidComponent()
    {
        Boid b = gameObject.AddComponent<Boid>();
        boid = b;
        return b;
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
        FishContainer container = other.GetComponent<FishContainer>();
        if(container != null)
        {
            container.Add(this);
            // Don't enable boid script yet if the fish is still attatched to a hook
            if(boid != null && attatchedHook != null)
            {
                boid.enabled = false;
            }
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        FishFood fishFood = other.GetComponent<FishFood>();
        if(fishFood != null)
        {
            Eat(fishFood);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        FishContainer container = other.GetComponent<FishContainer>();
        if (container != null)
        {
            container.Remove(this);
            rb.useGravity = true;
        }
    }

    public IEnumerator TugBobber(Rigidbody bobber)
    {
        for (int i = 0; i < 3; i++)
        {
            bobber.AddForce(Vector3.down * 300);
            yield return new WaitForSeconds(1);
            if (container == null)
            {
                yield break;
            }
        }
        DetachHook();
    }

    public void AttachHook(Hook hook)
    {
        attatchedHook = hook;
        if (boid != null)
        {
            boid.enabled = false;
        }
        rb.isKinematic = true;
    }

    /// <summary>
    /// Removes fish from hook. TODO: this function is not sufficient on it's own. You must also detatch the hook from the fish (See Hook.cs)
    /// </summary>
    public void DetachHook()
    {
        attatchedHook = null;
        transform.position += Vector3.right;
        if (boid != null)
        {
            boid.enabled = true;
        }
        if(container == null)
        {
            rb.isKinematic = false;
        }
    }

    private void Eat(FishFood fishFood)
    {
        if(container is BoidFishContainer)
        {
            ((BoidFishContainer)container).Remove(fishFood.transform);
        }
        Destroy(fishFood.gameObject);
        Debug.Log("Nom");
    }
}
