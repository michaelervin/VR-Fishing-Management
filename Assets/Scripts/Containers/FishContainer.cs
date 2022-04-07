using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class FishContainer : ObjectContainer<Fish>, ISavable
{
    [SerializeField]
    public List<FishData> fishData;

    protected virtual void Awake()
    {
        fishData = new List<FishData>();
        onAdd += OnContainerAdd;
        onRemove += OnContainerRemove;
    }

    private void OnContainerAdd(Fish fish)
    {
        fishData.Add(fish.data);
    }

    private void OnContainerRemove(Fish fish)
    {
        fishData.Remove(fish.data);
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
        objects.Clear();
        fishData.Clear();
    }

    class FishContainerSaveData : SaveData
    {
        public List<FishData> fishData;
    }
}
