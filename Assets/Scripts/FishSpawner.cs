using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public Fish SpawnRandomFish()
    {
        // TODO: maybe move to different class
        FishStaticData[] data = FishSpawnerUtility.GetAllStaticData().ToArray();
        FishStaticData selectedData = data[Random.Range(0, data.Length)];

        FishData fishData = new FishData();
        fishData.name = selectedData.name;
        fishData.nickName = RandomNickName();
        fishData.size = Random.Range(0.5f, 1.5f);

        Fish fish = FishSpawnerUtility.CreateFish(fishData);
        fish.transform.position = transform.position;
        fish.transform.rotation = transform.rotation;
        return fish;
    }

    string[] randomNames = new string[] { "George", "Fred", "Phil", "Tom", "Jacob", "Will", "Micheal", "Tristan", "Daniel" };
    public string RandomNickName()
    {
        return randomNames[Random.Range(0, randomNames.Length)];
    }
}
