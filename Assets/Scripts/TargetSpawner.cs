using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField]
    BoidFishContainer container;

    [SerializeField]
    Transform target;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpawnTarget();
            Debug.Log("added");
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnDespawnTarget();
            Debug.Log("removed");
        }
    }

    public void OnDespawnTarget()
    {
        container.Remove(target);
    }

    public void OnSpawnTarget()
    {
        container.Add(target);
    }
}
