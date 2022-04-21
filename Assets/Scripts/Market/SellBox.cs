using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Collider))]
public class SellBox<T> : MonoBehaviour where T : MonoBehaviour, IContainable, IDisplayable
{
    [SerializeField] MarketStand<T> stand;

    private void Awake()
    {
        Debug.Assert(stand != null);
    }

    private void OnTriggerEnter(Collider other)
    {
        IMarketable marketable = other.GetComponent<IMarketable>();
        if (marketable != null && marketable is T)
        {
            Interactable interactable = other.GetComponent<Interactable>();
            interactable?.attachedToHand?.DetachObject(interactable.gameObject);
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            stand.Buy(marketable as T);
        }
    }
}

public interface IMarketable
{
    double BaseCost();
}
