using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FishSpawnerUtility
{
    static Dictionary<string, GameObject> _prefabs;
    static Dictionary<string, GameObject> Prefabs
    {
        get
        {
            if (_prefabs == null)
            {
                _prefabs = new Dictionary<string, GameObject>();
                foreach (GameObject model in Resources.LoadAll<GameObject>("Prefabs/Fish"))
                {
                    _prefabs.Add(model.name, model);
                }
            }
            return _prefabs;
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
        Fish fish = Object.Instantiate(Prefabs["Fish"]).GetComponent<Fish>();
        fish.data = data;
        GameObject prefab;
        if (Prefabs.ContainsKey(fish.data.name) && fish.data.name != "Fish")
        {
            prefab = Prefabs[fish.data.name];
        }
        else
        {
            Debug.LogWarning($"Fish name invalid: {fish.data.name}. Renaming to Jerry...");
            prefab = Prefabs["Jerry"];
            fish.data.name = "Jerry";
        }
        GameObject model = Object.Instantiate(prefab);
        model.transform.parent = fish.transform;

        if (StaticData.ContainsKey(fish.data.name))
        {
            fish.staticData = StaticData[fish.data.name];
        }
        else
        {
            Debug.LogWarning($"Static data not found: {fish.data.name}. Defaulting to Jerry...");
            fish.staticData = StaticData["Jerry"];
        }

        return fish;
    }
}
