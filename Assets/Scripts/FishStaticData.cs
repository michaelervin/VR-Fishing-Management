using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FishStaticData : ScriptableObject
{
    public GameObject modelPrefab;
    public Sprite sprite;
    public double cost;
    public List<string> targetTypes;
}
