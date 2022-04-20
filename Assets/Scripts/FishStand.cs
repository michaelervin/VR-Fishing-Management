using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishStand : MonoBehaviour
{
    [SerializeField] MarketDisplay display;
    [SerializeField] FishSpawner spawner;
    [SerializeField] int availableCount;
    [SerializeField] float spacing;
    [SerializeField] GameObject pointer;

    List<Fish> availableFish = new List<Fish>();
    int index = 0;

    private void Start()
    {
        for (int i=0; i<availableCount; i++)
        {
            Fish fish = spawner.SpawnRandomFish();
            fish.transform.position += -fish.transform.right * i * spacing;
            availableFish.Add(fish);
        }
        display.SetDisplay(availableFish[index]);
    }

    private void Update()
    {
        pointer.transform.position = availableFish[index].transform.position + Vector3.up / 2;
    }

    public void NextIndex()
    {
        index++;
        if(index > availableFish.Count - 1)
        {
            index = 0;
        }
        display.SetDisplay(availableFish[index]);
    }

    public void PreviousIndex()
    {
        index--;
        if(index < 0)
        {
            index = availableFish.Count - 1;
        }
        display.SetDisplay(availableFish[index]);
    }
}
