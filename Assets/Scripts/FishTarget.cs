using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Rigidbody))]
public class FishTarget : MonoBehaviour, IContainable, IDisplayable, IAttachable
{
    public FishTargetType type;
    public FishTargetStaticData staticData;

    float IContainable.RequiredSpace => 0;

    [HideInInspector] public Rigidbody rb;

    BoidFishContainer container;

    private Follow follow;
    private Hook attachedHook;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnDestroy()
    {
        container?.Remove(this);
        attachedHook?.Detach(this);
    }

    void IContainable.Contain<T>(ObjectContainer<T> container)
    {
        Debug.Assert(container is BaitContainer);
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        BoidFishContainer container = other.GetComponent<BoidFishContainer>();
        if (container != null)
        {
            this.container = container;
            container.Add(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        BoidFishContainer container = other.GetComponent<BoidFishContainer>();
        if (container != null)
        {
            this.container = null;
            container.Remove(this);
        }
    }
    
    // TODO: this _hand thing sucks
    public Hand _hand;
    private void OnAttachedToHand(Hand hand)
    {
        if (attachedHook != null)
        {
            attachedHook.Detach(this);
        }
        _hand = hand;
    }

    private void OnDetachedToHand(Hand hand)
    {
        hand = null;
        rb.isKinematic = false;
    }

    void IContainable.Release()
    {
        rb.useGravity = true;
    }

    DisplayInfo IDisplayable.GetDisplayInfo()
    {
        DisplayInfo info = new DisplayInfo();
        info.image = null;
        info.text = type.ToString();
        return info;
    }

    void IAttachable.Attach(Hook hook)
    {
        follow = gameObject.AddComponent<Follow>();
        follow.followTransform = hook.transform;
        attachedHook = hook;
        if (staticData.hideHook)
        {
            hook.Visable = false;
        }
    }

    void IAttachable.Detach()
    {
        Destroy(follow);
        follow = null;

        if (staticData.hideHook)
        {
            // TODO: This doesn't account for other attached objects making it invisible
            attachedHook.Visable = true;
        }

        attachedHook = null;
    }
}

public enum FishTargetType
{
    Unknown,
    Insect,
    SmallFish,
    Worm,
    CrankBait,
    MinnowBait,
}