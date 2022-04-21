using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FishStand : MarketStand<Fish>
{
    [SerializeField] string[] allowedFish;

    private void Start()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            Fish fish = SpawnRandomFish();
            fish.transform.position = inventorySlots[i].position;
            fish.transform.rotation = inventorySlots[i].rotation;
            availableItems.Add(fish);
        }
        RefreshDisplay();
    }

    public Fish SpawnRandomFish()
    {
        FishStaticData[] data = FishSpawnerUtility.GetAllStaticData().ToArray();
        FishStaticData selectedData = data[Random.Range(0, data.Length)];
        while (!allowedFish.Contains(selectedData.name))
        {
            selectedData = data[Random.Range(0, data.Length)];
        }

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
