using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Fish : MonoBehaviour, IContainable, IDisplayable
{
    public FishData data;

    public FishContainer container;

    /// <summary>
    /// The boid script attatched to the fish. This is null if the fish is not in a boid managed environment.
    /// </summary>
    Boid boid;
    Rigidbody rb;
    Hook attatchedHook;
    bool hookInvulnerability;

    float IContainable.RequiredSpace => data.size;

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
        FishTarget fishFood = other.GetComponent<FishTarget>();
        if(fishFood != null)
        {
            Eat(fishFood);
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

    public bool AttachHook(Hook hook)
    {
        if (hookInvulnerability)
        {
            Debug.LogWarning("Failed to attach hook: the fish is invulnerable");
            return false;
        }
        attatchedHook = hook;
        if (boid != null)
        {
            boid.enabled = false;
        }
        rb.isKinematic = true;
        return true;
    }

    public void DetachHook()
    {
        attatchedHook.attachedFish = null;
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

    private void Eat(FishTarget fishFood)
    {
        if(container is BoidFishContainer)
        {
            ((BoidFishContainer)container).Remove(fishFood.transform);
        }
        Destroy(fishFood.gameObject);
        Debug.Log("Nom");
    }

    private IEnumerator HookInvulnerability(float seconds)
    {
        hookInvulnerability = true;
        yield return new WaitForSeconds(seconds);
        hookInvulnerability = false;
    }

    private void OnAttachedToHand(Hand hand)
    {
        if (attatchedHook != null)
        {
            DetachHook();
            StartCoroutine(HookInvulnerability(3));
        }
        rb.isKinematic = true;
    }

    private void OnDetachedFromHand(Hand hand)
    {
        rb.isKinematic = false;
    }

    void IContainable.Contain<T>(ObjectContainer<T> container)
    {
        Debug.Assert(container is FishContainer);
        this.container = container as FishContainer;
        if (boid != null && attatchedHook != null)
        {
            boid.enabled = false;
        }
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    void IContainable.Release()
    {
        container = null;
        rb.useGravity = true;
    }

    DisplayInfo IDisplayable.GetDisplayInfo()
    {
        DisplayInfo info = new DisplayInfo();
        info.text = data.nickName;
        info.image = FishDatabase.GetSprite(data.name);
        return info;
    }
}
