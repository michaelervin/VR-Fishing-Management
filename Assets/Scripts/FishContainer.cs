using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class FishContainer : MonoBehaviour
{
    public float capacity;
    public float usedCapacity;

    [SerializeField]
    List<Fish> fish;

    public Fish this[int value]
    {
        get => fish[value];
    }

    private void Awake()
    {
        fish = new List<Fish>();
    }

    public bool HasSpace(Fish fish)
    {
        return usedCapacity + fish.data.size <= capacity;
    }

    public virtual void Add(Fish fish)
    {
        if(!HasSpace(fish))
        {
            throw new InvalidOperationException("Could not add fish: the container is full!");
        }
        this.fish.Add(fish);
        fish.transform.parent = transform;
        fish.container = this;
        usedCapacity += fish.data.size;
    }

    public virtual void Remove(Fish fish)
    {
        this.fish.Remove(fish);
        fish.transform.parent = null;
        fish.container = null;
        usedCapacity -= fish.data.size;
    }
}
