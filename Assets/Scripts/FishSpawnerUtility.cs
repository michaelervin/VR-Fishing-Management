using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FishSpawnerUtility
{
    static GameObject _basePrefab;
    static GameObject BasePrefab
    {
        get
        {
            if (_basePrefab == null)
            {
                _basePrefab = Resources.Load<GameObject>("StaticData/Fish/BaseFish");
            }
            return _basePrefab;
        }
    }

    static Dictionary<string, FishStaticData> _staticData;
    static Dictionary<string, FishStaticData> StaticData
    {
        get
        {
            if (_staticData == null)
            {
                _staticData = new Dictionary<string, FishStaticData>();
                foreach (FishStaticData data in Resources.LoadAll<FishStaticData>("StaticData/Fish"))
                {
                    _staticData.Add(data.name, data);
                }
            }
            return _staticData;
        }
    }

    public static Fish CreateFish(FishData data)
    {
        Fish fish = Object.Instantiate(BasePrefab).GetComponent<Fish>();
        fish.data = data;
        GameObject prefab;
        if (StaticData.ContainsKey(fish.data.name))
        {
            fish.staticData = StaticData[fish.data.name];
            prefab = StaticData[fish.data.name].modelPrefab;
        }
        else
        {
            Debug.LogWarning($"Static data not found: {fish.data.name}. Defaulting to Jerry...");
            fish.staticData = StaticData["Jerry"];
            prefab = StaticData["Jerry"].modelPrefab;
            fish.data.name = "Jerry";
        }
        GameObject model = Object.Instantiate(prefab);
        model.transform.parent = fish.transform;

        return fish;
    }

    public static IEnumerable<FishStaticData> GetAllStaticData()
    {
        return StaticData.Values;
    }
}
