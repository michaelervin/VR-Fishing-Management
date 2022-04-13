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
        return target;
    }
}

