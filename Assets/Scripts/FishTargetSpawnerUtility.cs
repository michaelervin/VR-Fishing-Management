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

    public static FishTarget CreateTarget(FishTargetData data)
    {
        FishTarget target = Object.Instantiate(BasePrefab).GetComponent<FishTarget>();
        target.data = data;
        GameObject prefab;
        if (StaticData.ContainsKey(data.type != null ? data.type : ""))
        {
            target.staticData = StaticData[target.data.type];
            prefab = StaticData[target.data.type].modelPrefab;
        }
        else
        {
            Debug.LogWarning($"Static data not found: {target.data.type}. Defaulting to Jim...");
            target.staticData = StaticData["Jim"];
            target.data.type = "Jim";
            prefab = StaticData["Jim"].modelPrefab;
        }
        GameObject model = Object.Instantiate(prefab);
        model.transform.parent = target.transform;

        return target;
    }

    public static IEnumerable<FishTargetStaticData> GetAllStaticData()
    {
        return StaticData.Values;
    }
}

