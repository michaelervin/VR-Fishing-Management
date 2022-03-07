using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Hook : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] float hookChance = 1;
    [SerializeField] GameObject baitPrefab;

    public void AddBait()
    {
        GameObject bait = Instantiate(baitPrefab);
        bait.GetComponent<Follow>().followTransform = transform;

    }

    private void OnTriggerEnter(Collider other)
    {
        Fish fish = other.GetComponent<Fish>();
        if (fish != null)
        {
            if (Random.Range(0f, 1f) < hookChance)
            {
                fish.AttachHook(this);
            }
        }
    }
}
