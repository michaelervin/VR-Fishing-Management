using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InspectionTable : MonoBehaviour
{
    [SerializeField] DetailedDisplay display;

    private void OnCollisionEnter(Collision collision)
    {
        Fish fish = collision.gameObject.GetComponent<Fish>();
        if (fish)
        {
            display.SetDisplay(fish);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        display.SetDisplay(null);
    }
}
