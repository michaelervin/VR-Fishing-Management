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

    public static FishTarget CreateTarget(FishTargetType data)
    {
        FishTarget fish = Object.Instantiate(Prefabs["FishTarget"]).GetComponent<FishTarget>();
        fish.type = data;
        GameObject prefab;
        if (Prefabs.ContainsKey(fish.type.ToString()) && fish.type.ToString() != "FishTarget")
        {
            prefab = Prefabs[fish.type.ToString()];
        }
        else
        {
            Debug.LogWarning($"Target name invalid: {fish.type}. Renaming to Larry...");
            prefab = Prefabs["Larry"];
        }
        GameObject model = Object.Instantiate(prefab);
        model.transform.parent = fish.transform;
        return fish;
    }
}

