using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class SellFishBox : MonoBehaviour
{
    public UnityEvent<Fish> OnCollide;

    private void OnTriggerEnter(Collider other)
    {
        Fish fish = other.GetComponent<Fish>();
        if (fish)
        {
            OnCollide?.Invoke(fish);
        }
    }
}
