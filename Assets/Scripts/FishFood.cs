using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFood : MonoBehaviour
{
    public FishFoodType type;

    private void OnTriggerEnter(Collider other)
    {
        BoidFishContainer container = other.GetComponent<BoidFishContainer>();
        if (container != null)
        {
            container.Add(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        BoidFishContainer container = other.GetComponent<BoidFishContainer>();
        if (container != null)
        {
            container.Remove(transform);
        }
    }
}

public enum FishFoodType
{
    Insect,
    SmallFish,
    Worms,
}