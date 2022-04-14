using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class SellBox : MonoBehaviour
{
    public UnityEvent<FishTarget> OnCollide;

    private void OnTriggerEnter(Collider other)
    {
        FishTarget bait = other.GetComponent<FishTarget>();
        if (bait)
        {
            OnCollide?.Invoke(bait);
        }
    }
}
