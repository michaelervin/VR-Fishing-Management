using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FishTargetSpawnerUtility
{
    static Dictionary<string, GameObject> _prefabs;
    static Dictionary<string, GameObject> Prefabs
    {
        get
        {
            if (_prefabs == null)
            {
                _prefabs = new Dictionary<string, GameObject>();
                foreach (GameObject model in Resources.LoadAll<GameObject>("Prefabs/FishTargets"))
                {
                    _prefabs.Add(model.name, model);
                }
            }
            return _prefabs;
        }
    }
    static Dictionary<string, FishTargetStaticData> _staticData;
    static Dictionary<string, FishTargetStaticData> StaticData
    {
        get
        {
            if (_staticData == null)
            {
                _staticData = new Dictionary<string, FishTargetStaticData>();
                foreach (FishTargetStaticData data in Resources.LoadAll<FishTargetStaticData>("StaticData/FishTargets"))
                {
                    _staticData.Add(data.name, data);
                }
            }
            return _staticData;
        }
    }

    public static FishTarget CreateTarget(FishTargetType type)
    {
        FishTarget target = Object.Instantiate(Prefabs["FishTarget"]).GetComponent<FishTarget>();
        target.type = type;
        GameObject prefab;
        if (Prefabs.ContainsKey(target.type.ToString()) && target.type.ToString() != "FishTarget")
        {
            prefab = Prefabs[target.type.ToString()];
        }
        else
        {
            Debug.LogWarning($"Target name invalid: {target.type}. Renaming to Larry...");
            prefab = Prefabs["Larry"];
        }
        GameObject model = Object.Instantiate(prefab);
        model.transform.parent = target.transform;

        if (StaticData.ContainsKey(target.type.ToString()))
        {
            target.staticData = StaticData[target.type.ToString()];
        }
        else
        {
            Debug.LogWarning($"Static data not found: {target.type}. Defaulting to Larry...");
            target.staticData = StaticData["Larry"];
        }

        return target;
    }
}

