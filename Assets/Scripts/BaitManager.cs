using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitManager : MonoBehaviour
{
    [SerializeField] private GameObject proximityPoint;
    [SerializeField] private float proximityDistance = 0.5f;
    [SerializeField] private FishTarget bait;
    [SerializeField] private BaitSpawner baitSpawner;
    [SerializeField] private Hook hook;

    public int baitsAvailable;

    private void Start()
    {
        bait = baitSpawner.Spawn();
    }
    private void FixedUpdate()
    {
        if (baitsAvailable != 0)     
        {
            if (Vector3.Distance(bait.transform.position, proximityPoint.transform.position) <= proximityDistance)
            {
                if (hook.AddBait(bait))
                {
                    baitsAvailable--;
                    bait = baitSpawner.Spawn();
                }
            }
        }
    }
}
