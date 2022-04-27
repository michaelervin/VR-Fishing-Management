using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Fish : MonoBehaviour, IContainable, IDisplayable, IAttachable, IMarketable
{
    public FishData data;
    public FishStaticData staticData;

    public FishContainer container;

    /// <summary>
    /// The boid script attatched to the fish. This is null if the fish is not in a boid managed environment.
    /// </summary>
    Boid boid;
    Rigidbody rb;
    Interactable interactable;
    Hook attachedHook;

    float IContainable.RequiredSpace => data.size;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        boid = GetComponent<Boid>();
        interactable = GetComponent<Interactable>();
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
        if(fishFood != null && fishFood.staticData.consumable && staticData.targetTypes.Contains(fishFood.data.type))
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
        if(interactable.attachedToHand) interactable.attachedToHand.DetachObject(gameObject);

        attachedHook = hook;
        if (boid != null)
        {
            boid.enabled = false;
        }
        rb.isKinematic = true;

        if (hook.Lure.staticData.consumable)
        {
            Eat(hook.Lure);
        }

        if(boid) StartCoroutine(TugHook(hook));
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
        fishFood.OnEat();
        Debug.Log("Nom");
    }

    private void OnAttachedToHand(Hand hand)
    {
        if (attachedHook != null)
        {
            attachedHook.Detach(this);
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
        info.title = data.nickName;
        info.subTitle = data.name;
        info.description = staticData.description;
        info.cost = staticData.cost;
        info.sprite = staticData.sprite;
        return info;
    }

    double IMarketable.BaseCost()
    {
        return staticData.cost;
    }
}
