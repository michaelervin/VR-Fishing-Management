using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Collider))]
public class SellBox : MonoBehaviour
{
    [SerializeField] FishStand stand;

    private void Awake()
    {
        Debug.Assert(stand != null);
    }

    private void OnTriggerEnter(Collider other)
    {
        IMarketable marketable = other.GetComponent<IMarketable>();
        if (marketable != null)
        {
            Interactable interactable = other.GetComponent<Interactable>();
            interactable?.attachedToHand?.DetachObject(interactable.gameObject);
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            stand.Buy(marketable as Fish); // TODO: fix
        }
    }
}

public interface IMarketable
{
    double BaseCost();
}
