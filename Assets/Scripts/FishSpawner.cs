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
