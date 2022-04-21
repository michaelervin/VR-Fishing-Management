using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaitStand : MarketStand<FishTarget>
{
    [SerializeField] string[] allowedBait;

    private void Start()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            var baitData = FishTargetSpawnerUtility.GetAllStaticData().ToArray();
            var selectedBait = baitData[Random.Range(0, baitData.Length)];
            while (!allowedBait.Contains(selectedBait.name))
            {
                selectedBait = baitData[Random.Range(0, baitData.Length)];
            }

            var fishTargetData = new FishTargetData();
            fishTargetData.type = selectedBait.name;
            FishTarget bait = FishTargetSpawnerUtility.CreateTarget(fishTargetData);
            bait.transform.position = inventorySlots[i].position;
            bait.transform.rotation = inventorySlots[i].rotation;
            availableItems.Add(bait);
        }
        RefreshDisplay();
    }
}
