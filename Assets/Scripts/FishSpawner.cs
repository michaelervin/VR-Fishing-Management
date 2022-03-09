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
    int spawnOnStartCount = 5;
    [SerializeField]
    float spawnTimeIncrement = 1f;

    private IEnumerator Start()
    {
        for(int i=0; i<spawnOnStartCount; i++)
        {
            SpawnFish();
            yield return new WaitForSeconds(spawnTimeIncrement);
        }
    }

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
        fish.transform.position = transform.position;
    }

}
