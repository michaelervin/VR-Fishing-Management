using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FishStaticData : ScriptableObject
{
    public GameObject modelPrefab;
    public List<FishTargetType> targetTypes;
}
