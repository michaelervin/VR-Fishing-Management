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

    public void DespawnFish()
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

    public void SpawnFish()
    {
        Fish fish = Instantiate(prefab);
        fish.transform.rotation = UnityEngine.Random.rotation;
    }

}
