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
        if (target)
        {
            container.Remove(target.transform);
            Destroy(target);
        }
        else
        {
            Debug.Log("No longer have the last target spawned reference");
        }
    }

    public void SpawnTarget()
    {
        target = new GameObject("Target");
        container.Add(target.transform);
    }
}
