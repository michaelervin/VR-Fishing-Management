using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitContainer : ObjectContainer<FishTarget>, ISavable
{
    public List<FishTargetData> baitData;

    protected virtual void Awake()
    {
        baitData = new List<FishTargetData>();
        onAdd += OnContainerAdd;
        onRemove += OnContainerRemove;
    }

    private void OnContainerAdd(FishTarget target)
    {
        baitData.Add(target.data);
    }

    private void OnContainerRemove(FishTarget target)
    {
        baitData.Remove(target.data);
    }

    Type ISavable.GetSaveDataType()
    {
        return typeof(BaitContainerSaveData);
    }

    void ISavable.OnFinishLoad()
    {
        foreach (FishTargetData data in baitData)
        {
            FishTarget target = FishTargetSpawnerUtility.CreateTarget(data);
            if(target) target.transform.position = transform.position;
        }

        // Arrays will be repopulated on collision with fish
        objects.Clear();
        baitData.Clear();
    }

    class BaitContainerSaveData : SaveData
    {
        public List<FishTargetData> baitData;
    }
}
