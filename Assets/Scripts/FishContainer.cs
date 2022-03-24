using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FishContainer : MonoBehaviour, ISavable
{
    public float capacity;
    public float usedCapacity;

    [SerializeField]
    public List<FishData> fishData;
    public List<Fish> fish;

    protected virtual void Awake()
    {
        fishData = new List<FishData>();
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
        fishData.Add(fish.data);
        fish.transform.parent = transform;
        fish.container = this;
        usedCapacity += fish.data.size;
    }

    public virtual void Remove(Fish fish)
    {
        this.fish.Remove(fish);
        fishData.Remove(fish.data);
        fish.transform.parent = null;
        fish.container = null;
        usedCapacity -= fish.data.size;
    }

    public Type GetSaveDataType()
    {
        return typeof(FishContainerSaveData);
    }

    public void OnFinishLoad()
    {
        foreach(FishData data in fishData)
        {
            Fish fish = FishSpawnerUtility.CreateFish(data);
            fish.transform.position = transform.position;
        }

        // Arrays will be repopulated on collision with fish
        fish.Clear();
        fishData.Clear();
    }

    class FishContainerSaveData : SaveData
    {
        public List<FishData> fishData;
    }
}
