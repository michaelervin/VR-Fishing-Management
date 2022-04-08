using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitContainer : ObjectContainer<FishTarget>, ISavable
{
    public List<FishTargetType> baitData;

    protected virtual void Awake()
    {
        baitData = new List<FishTargetType>();
        onAdd += OnContainerAdd;
        onRemove += OnContainerRemove;
    }

    private void OnContainerAdd(FishTarget target)
    {
        baitData.Add(target.type);
    }

    private void OnContainerRemove(FishTarget target)
    {
        baitData.Remove(target.type);
    }

    Type ISavable.GetSaveDataType()
    {
        return typeof(BaitContainerSaveData);
    }

    void ISavable.OnFinishLoad()
    {
        foreach (FishTargetType data in baitData)
        {
            FishTarget target = FishTargetSpawnerUtility.CreateTarget(data);
            target.transform.position = transform.position;
        }

        // Arrays will be repopulated on collision with fish
        objects.Clear();
        baitData.Clear();
    }

    class BaitContainerSaveData : SaveData
    {
        public List<FishTargetType> baitData;
    }
}
