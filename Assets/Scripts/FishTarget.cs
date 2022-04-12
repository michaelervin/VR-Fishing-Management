using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Rigidbody))]
public class FishTarget : MonoBehaviour, IContainable, IDisplayable
{
    public FishTargetType type;

    float IContainable.RequiredSpace => 0;

    [HideInInspector] public Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
            container.Add(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        BoidFishContainer container = other.GetComponent<BoidFishContainer>();
        if (container != null)
        {
            container.Remove(this);
        }
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
}

public enum FishTargetType
{
    Unknown,
    Insect,
    SmallFish,
    Worms,
}