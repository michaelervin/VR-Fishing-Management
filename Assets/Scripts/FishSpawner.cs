using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField]
    Fish prefab;
    [SerializeField]
    BoidFishContainer container;

    [SerializeField]
    int spawnOnStartCount;

    public IEnumerator Start()
    {
        for (int i = 0; i < spawnOnStartCount; i++)
        {
            OnSpawnFish();
            yield return new WaitForSeconds(1f);
        }
    }

    public void OnDespawnFish()
    {
        try
        {
            Fish fish = container[0];
            container.Remove(fish);
            Destroy(fish.gameObject);
        }
        catch (ArgumentOutOfRangeException)
        {
            Debug.Log("There's no fish in this container!");
        }
    }

    public void OnSpawnFish()
    {
        Fish fish = Instantiate(prefab);
        if (container.HasSpace(fish))
        {
            container.Add(fish);
        }
        else
        {
            Debug.Log("The container is full!");
            Destroy(fish.gameObject);
        }
    }

}
