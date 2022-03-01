using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishContainer : MonoBehaviour
{
    public int capacity;

    List<Fish> fish;

    public void Add(Fish fish)
    {
        if(this.fish.Count == capacity)
        {
            throw new InvalidOperationException();
        }
        this.fish.Add(fish);
        fish.transform.parent = transform;
        fish.container = this;
    }

    public void Remove(Fish fish)
    {
        this.fish.Remove(fish);
        fish.transform.parent = null;
        fish.container = null;
    }
}
