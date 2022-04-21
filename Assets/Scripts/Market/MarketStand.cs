using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class MarketStand<T> : MonoBehaviour where T : MonoBehaviour, IContainable, IDisplayable
{
    [SerializeField] JerryBank bank;
    [SerializeField] MarketDisplay display;
    [SerializeField] GameObject pointer;
    [SerializeField] GameObject buyButton;
    [SerializeField] GameObject cantBuyText;
    [SerializeField] 
    protected List<Transform> inventorySlots;

    Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.TurnOnKinematic;
    protected List<T> availableItems = new List<T>();
    int index = 0;

    T SelectedItem
    {
        get => availableItems[index];
        set => availableItems[index] = value;
    }    

    private void Update()
    {
        if (SelectedItem)
        {
            buyButton.SetActive(true);
            pointer.transform.position = SelectedItem.transform.position + Vector3.up / 2;
        }
        else
        {
            buyButton.SetActive(false);
            pointer.transform.position = inventorySlots[index].transform.position + Vector3.up / 2;
        }
    }

    /// <summary>
    /// Sell to the player and attach to the Hand
    /// </summary>
    /// <param name="hand"></param>
    public void Sell(Hand hand)
    {
        double cost = (SelectedItem as IMarketable).BaseCost();
        if (cost <= bank.jerryBucks)
        {
            bank.jerryBucks -= cost;
            SelectedItem.transform.position = hand.transform.position;
            hand.AttachObject(SelectedItem.gameObject, hand.GetBestGrabbingType(), attachmentFlags);
            SelectedItem = null;
            RefreshDisplay();
        }
        else
        {
            cantBuyText.SetActive(true);
        }
    }

    /// <summary>
    /// Buy from the player
    /// </summary>
    /// <param name="fish"></param>
    public void Buy(T item)
    {
        bank.jerryBucks += (item as IMarketable).BaseCost();
        bool foundSlot = false;
        for (int i = 0; i < availableItems.Count; i++)
        {
            if (availableItems[i] == null)
            {
                item.transform.position = inventorySlots[i].position;
                item.transform.rotation = inventorySlots[i].rotation;
                availableItems[i] = item;
                foundSlot = true;
                break;
            }
        }
        if (!foundSlot)
        {
            Destroy(item.gameObject);
        }
        RefreshDisplay();
    }

    protected void RefreshDisplay()
    {
        display.SetDisplay(SelectedItem);
        cantBuyText.SetActive(false);
    }

    public void NextIndex()
    {
        index++;
        if(index > availableItems.Count - 1)
        {
            index = 0;
        }
        RefreshDisplay();
    }

    public void PreviousIndex()
    {
        index--;
        if(index < 0)
        {
            index = availableItems.Count - 1;
        }
        RefreshDisplay();
    }
}
