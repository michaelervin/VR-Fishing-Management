using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachBait : MonoBehaviour
{
    [SerializeField] private GameObject proximityPoint;
    [SerializeField] private float proximityDistance = 0.5f;
    [SerializeField] private GameObject bait;
    [SerializeField] private GameObject baitSpawner;
    [SerializeField] private Hook hook;

    public bool baitedHook;
    public int baitsAvailable;

    private void Start()
    {
        baitedHook = false;
        baitsAvailable = 2;
    }
    private void Update()
    { 
        if (Vector3.Distance(bait.transform.position,
                proximityPoint.transform.position) <= proximityDistance)
        {

            if(baitedHook == false)
            {
                baitsAvailable = baitsAvailable -= 1;
                hook.AddBait();
                baitedHook = true;
                bait.transform.position = baitSpawner.transform.position;
            }
            if(baitsAvailable == 0)
            {
                bait.SetActive(false);
            }
        }
    }
}
