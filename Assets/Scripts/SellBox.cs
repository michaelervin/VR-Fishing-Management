using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Collider))]
public class SellBox : MonoBehaviour
{
    [SerializeField] JerryBank bank;

    private void Awake()
    {
        Debug.Assert(bank != null);
    }

    private void OnTriggerEnter(Collider other)
    {
        IMarketable marketable = other.GetComponent<IMarketable>();
        if (marketable != null)
        {
            bank.jerryBucks += marketable.BaseCost();
            Interactable interactable = other.GetComponent<Interactable>();
            interactable?.attachedToHand.DetachObject(interactable.gameObject);
            Destroy(other.gameObject);
        }
    }
}

public interface IMarketable
{
    double BaseCost();
}
