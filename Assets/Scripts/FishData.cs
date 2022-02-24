using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
internal class FishData : ScriptableObject
{
    public string description;

    public List<FishFood> acceptableFood;
}