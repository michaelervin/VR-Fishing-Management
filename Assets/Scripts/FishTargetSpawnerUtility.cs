using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FishTargetSpawnerUtility
{
    static GameObject _basePrefab;
    static GameObject BasePrefab
    {
        get
        {
            if (_basePrefab == null)
            {
                _basePrefab = Resources.Load<GameObject>("StaticData/FishTargets/BaseFishTarget");
            }
            return _basePrefab;
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

    public static FishTarget CreateTarget(string type)
    {
        return CreateTarget((FishTargetType)System.Enum.Parse(typeof(FishTargetType), type));
    }

    public static FishTarget CreateTarget(FishTargetType type)
    {
        FishTarget target = Object.Instantiate(BasePrefab).GetComponent<FishTarget>();
        target.type = type;
        GameObject prefab;
        if (StaticData.ContainsKey(target.type.ToString()))
        {
            target.staticData = StaticData[target.type.ToString()];
            prefab = StaticData[target.type.ToString()].modelPrefab;
        }
        else
        {
            Debug.LogWarning($"Static data not found: {target.type}. Defaulting to Larry...");
            target.staticData = StaticData["Larry"];
            prefab = StaticData["Larry"].modelPrefab;
        }
        GameObject model = Object.Instantiate(prefab);
        model.transform.parent = target.transform;

        return target;
    }

    public static FishTargetStaticData GetStaticData(FishTargetType type)
    {
        if (StaticData.ContainsKey(type.ToString()))
        {
            return StaticData[type.ToString()];
        }
        else
        {
            Debug.LogWarning($"Static data not found: {type}. Defaulting to Larry...");
            return StaticData["Larry"];
        }
    }
}

