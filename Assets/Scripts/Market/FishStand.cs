using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishStand : MarketStand<Fish>
{
    [SerializeField] FishSpawner spawner;

    private void Start()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            Fish fish = spawner.SpawnRandomFish();
            fish.transform.position = inventorySlots[i].position;
            fish.transform.rotation = inventorySlots[i].rotation;
            availableItems.Add(fish);
        }
        RefreshDisplay();
    }
}
