using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFood : MonoBehaviour
{
    public FishFoodType type;
}

public enum FishFoodType
{
    Insect,
    SmallFish,
    Worms,
}