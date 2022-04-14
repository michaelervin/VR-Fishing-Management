using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Fish : MonoBehaviour, IContainable, IDisplayable, IAttachable
{
    public FishData data;
    public FishStaticData staticData;

    public FishContainer container;

    /// <summary>
    /// The boid script attatched to the fish. This is null if the fish is not in a boid managed environment.
    /// </summary>
    Boid boid;
    Rigidbody rb;
    Hook attachedHook;

    float IContainable.RequiredSpace => data.size;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        boid = GetComponent<Boid>();
    }

    public Boid AddBoidComponent()
    {
        Boid b = gameObject.AddComponent<Boid>();
        b.pursuedTargetTypes = staticData.targetTypes;
        boid = b;
        return b;
    }

    private void FixedUpdate()
    {
        if (attachedHook != null)
        {
            rb.MovePosition(attachedHook.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        FishTarget fishFood = other.GetComponent<FishTarget>();
        if(fishFood != null && fishFood.staticData.consumable && staticData.targetTypes.Contains(fishFood.type))
        {
            Eat(fishFood);
        }
    }

    public IEnumerator TugHook(Hook hook)
    {
        Rigidbody bobber = hook.bobber;
        AudioSource.PlayClipAtPoint(hook.fishingBellSound, bobber.transform.position);
        for (int i = 0; i < 3; i++)
        {
            bobber.AddForce(Vector3.down * 300);
            yield return new WaitForSeconds(1);
            if (container == null)
            {
                yield break;
            }
        }
        hook.Detach(this);
    }

    void IAttachable.Attach(Hook hook)
    {
        attachedHook = hook;
        if (boid != null)
        {
            boid.enabled = false;
        }
        rb.isKinematic = true;

        StartCoroutine(TugHook(hook));
    }

    void IAttachable.Detach()
    {
        attachedHook = null;
        if (boid != null)
        {
            boid.enabled = true;
        }
        if (container == null)
        {
            rb.isKinematic = false;
        }
    }

    private void Eat(FishTarget fishFood)
    {
        if(container is BoidFishContainer)
        {
            ((BoidFishContainer)container).Remove(fishFood);
        }
        Destroy(fishFood.gameObject);
        Debug.Log("Nom");
    }

    // TODO: this _hand thing sucks
    public Hand _hand;
    private void OnAttachedToHand(Hand hand)
    {
        if (attachedHook != null)
        {
            attachedHook.Detach(this);
        }
        rb.isKinematic = true;
        _hand = hand;
    }

    private void OnDetachedFromHand(Hand hand)
    {
        rb.isKinematic = false;
        _hand = null;
    }

    void IContainable.Contain<T>(ObjectContainer<T> container)
    {
        Debug.Assert(container is FishContainer);
        this.container = container as FishContainer;
        if (boid != null && attachedHook != null)
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
