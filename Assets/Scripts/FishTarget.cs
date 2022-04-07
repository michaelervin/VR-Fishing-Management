using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Rigidbody))]
public class FishTarget : MonoBehaviour, IContainable
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
            container.Add(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        BoidFishContainer container = other.GetComponent<BoidFishContainer>();
        if (container != null)
        {
            container.Remove(transform);
        }
    }

    void IContainable.Release()
    {
        rb.useGravity = true;
    }
}

public enum FishTargetType
{
    Insect,
    SmallFish,
    Worms,
}