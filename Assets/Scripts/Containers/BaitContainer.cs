using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitContainer : ObjectContainer<FishTarget>, ISavable
{
    public List<FishTargetType> data;

    protected virtual void Awake()
    {
        data = new List<FishTargetType>();
        onAdd += OnContainerAdd;
        onRemove += OnContainerRemove;
    }

    private void OnContainerAdd(FishTarget target)
    {
        data.Add(target.type);
    }

    private void OnContainerRemove(FishTarget target)
    {
        data.Remove(target.type);
    }

    Type ISavable.GetSaveDataType()
    {
        return typeof(BaitContainerSaveData);
    }

    void ISavable.OnFinishLoad()
    {
        foreach (FishTargetType data in data)
        {
            FishTarget target = FishTargetSpawnerUtility.CreateTarget(data);
            target.transform.position = transform.position;
        }

        // Arrays will be repopulated on collision with fish
        objects.Clear();
        data.Clear();
    }

    class BaitContainerSaveData : SaveData
    {
        public List<FishTargetType> fishData;
    }
}
