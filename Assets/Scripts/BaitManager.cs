using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitManager : MonoBehaviour
{
    [SerializeField] private GameObject proximityPoint;
    [SerializeField] private float proximityDistance = 0.5f;
    [SerializeField] private GameObject bait;
    [SerializeField] private BaitSpawner baitSpawner;
    [SerializeField] private Hook hook;

    public bool baitedHook;
    public int baitsAvailable;

    private void Start()
    {
        bait = baitSpawner.Spawn();
        baitsAvailable = 2;
    }
    private void Update()
    {
        if (baitsAvailable != 0)     
        {
            if (Vector3.Distance(bait.transform.position, proximityPoint.transform.position) <= proximityDistance)
            {
                if (hook.AddBait())
                {
                    bait.SetActive(false);
                    Destroy(bait);
                    baitsAvailable = baitsAvailable -= 1;
                    bait = baitSpawner.Spawn();
                }
            }
        }
    }
}
