using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Utility behavior for spawning fish via the inspector.
/// </summary>
public class FishSpawner : MonoBehaviour
{
    [SerializeField]
    FishData fishData;
    [SerializeField]
    BoidFishContainer container;
    [SerializeField]
    int spawnOnStartCount = 5;
    [SerializeField]
    float spawnTimeIncrement = 1f;
    [SerializeField]
    float spawnTimeOffset = 0.5f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(spawnTimeOffset);
        for (int i = 0; i < spawnOnStartCount; i++)
        {
            yield return new WaitForSeconds(spawnTimeIncrement);
            if (container.usedCapacity + fishData.size <= container.capacity)
            {
                SpawnFish();
            }
            else
            {
                Debug.LogWarning("Attempted to spawn fish in full container, skipping...");
            }
        }
    }

    public void DespawnFish()
    {
        try
        {
            Fish fish = container.objects[0];
            container.Remove(fish);
            Destroy(fish.gameObject);
        }
        catch (System.ArgumentOutOfRangeException)
        {
            Debug.Log("There's no fish in this container!");
        }
    }

    public void SpawnFish()
    {
        Fish fish = FishSpawnerUtility.CreateFish(fishData);
        fish.transform.position = transform.position;
    }
}

public static class FishSpawnerUtility
{
    static Dictionary<string, GameObject> _prefabs;
    static Dictionary<string, GameObject> Prefabs
    {
        get
        {
            if(_prefabs == null)
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
        return fish;
    }
}
