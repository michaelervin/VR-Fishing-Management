using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Rigidbody))]
public class FishTarget : MonoBehaviour, IContainable, IDisplayable, IMarketable
{
    public FishTargetData data;
    public FishTargetStaticData staticData;

    float IContainable.RequiredSpace => 0;

    Rigidbody rb;
    Collider col;
    Interactable interactable;

    BoidFishContainer container;

    private Follow follow;
    private Hook attachedHook;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        interactable = GetComponent<Interactable>();
    }

    public void EnableCollider(bool enabled)
    {
        col.enabled = enabled;
    }

    private void OnDestroy()
    {
        if(container) container.Remove(this);
    }

    public void OnEat()
    {
        if (attachedHook) attachedHook.RemoveLure();
        Destroy(gameObject);
    }

    void IContainable.Contain<T>(ObjectContainer<T> container)
    {
        Debug.Assert(container is BaitContainer);
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
    
    private void OnAttachedToHand(Hand hand)
    {
        if (attachedHook != null)
        {
            attachedHook.RemoveLure();
        }
        rb.isKinematic = true;
    }

    private void OnDetachedFromHand(Hand hand)
    {
        rb.isKinematic = false;
    }

    void IContainable.Release()
    {
        rb.useGravity = true;
    }

    DisplayInfo IDisplayable.GetDisplayInfo()
    {
        DisplayInfo info = new DisplayInfo();
        info.sprite = staticData.sprite;
        info.title = data.type;
        info.cost = staticData.cost;
        return info;
    }

    public void AttachHook(Hook hook)
    {
        follow = gameObject.AddComponent<Follow>();
        follow.followTransform = hook.transform;
        attachedHook = hook;
        transform.rotation = hook.transform.rotation;
        if(interactable.attachedToHand) interactable.attachedToHand.DetachObject(gameObject);
    }

    public void DetachHook()
    {
        Destroy(follow);
        follow = null;

        attachedHook = null;
    }

    double IMarketable.BaseCost()
    {
        return staticData.cost;
    }
}
