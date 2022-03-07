using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidTargetSpawner : MonoBehaviour
{
    [SerializeField]
    BoidFishContainer container;

    [SerializeField]
    GameObject target;

    public void DespawnTarget()
    {
        container.Remove(target.transform);
        Destroy(target);
    }

    public void SpawnTarget()
    {
        target = new GameObject("Target");
        container.Add(target.transform);
    }
}
