using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FishTargetStaticData : ScriptableObject
{
    public GameObject modelPrefab;
    public Sprite sprite;
    public double cost;
    public bool hideHook;
    public bool consumable;
}
