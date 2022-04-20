using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class FishStand : MonoBehaviour
{
    [SerializeField] JerryBank bank;
    [SerializeField] MarketDisplay display;
    [SerializeField] FishSpawner spawner;
    [SerializeField] float spacing;
    [SerializeField] GameObject pointer;
    [SerializeField] GameObject buyButton;
    [SerializeField] GameObject cantBuyText;
    [SerializeField] List<Transform> inventorySlots;

    Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.TurnOnKinematic;
    List<Fish> availableFish = new List<Fish>();
    int index = 0;

    Fish SelectedFish
    {
        get => availableFish[index];
        set => availableFish[index] = value;
    }

    private void Start()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            Fish fish = spawner.SpawnRandomFish();
            fish.transform.position = inventorySlots[i].position;
            fish.transform.rotation = inventorySlots[i].rotation;
            availableFish.Add(fish);
        }
        RefreshDisplay();
    }

    private void Update()
    {
        if (SelectedFish)
        {
            buyButton.SetActive(true);
            pointer.transform.position = SelectedFish.transform.position + Vector3.up / 2;
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
        double cost = (SelectedFish as IMarketable).BaseCost();
        if (cost <= bank.jerryBucks)
        {
            bank.jerryBucks -= cost;
            SelectedFish.transform.position = hand.transform.position;
            hand.AttachObject(SelectedFish.gameObject, hand.GetBestGrabbingType(), attachmentFlags);
            SelectedFish = null;
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
    public void Buy(Fish fish)
    {
        bank.jerryBucks += (fish as IMarketable).BaseCost();
        for (int i = 0; i < availableFish.Count; i++)
        {
            if (availableFish[i] == null)
            {
                fish.transform.position = inventorySlots[i].position;
                fish.transform.rotation = inventorySlots[i].rotation;
                availableFish[i] = fish;
                break;
            }
        }
        RefreshDisplay();
    }

    private void RefreshDisplay()
    {
        display.SetDisplay(SelectedFish);
        cantBuyText.SetActive(false);
    }

    public void NextIndex()
    {
        index++;
        if(index > availableFish.Count - 1)
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
            index = availableFish.Count - 1;
        }
        RefreshDisplay();
    }
}
