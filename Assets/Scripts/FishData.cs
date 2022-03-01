using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FishData : ScriptableObject
{
    public string description;

    public List<FishFood> acceptableFood;
}