using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class FishStand : MonoBehaviour
{
    [SerializeField] JerryBank bank;
    [SerializeField] MarketDisplay display;
    [SerializeField] FishSpawner spawner;
    [SerializeField] int availableCount;
    [SerializeField] float spacing;
    [SerializeField] GameObject pointer;
    [SerializeField] GameObject buyButton;
    [SerializeField] GameObject cantBuyText;

    Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.TurnOnKinematic;
    List<Fish> availableFish = new List<Fish>();
    int index = 0;

    Fish SelectedFish
    {
        get
        {
            try
            {
                return availableFish[index];
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return null;
            }
        }
    }

    private void Start()
    {
        for (int i=0; i<availableCount; i++)
        {
            Fish fish = spawner.SpawnRandomFish();
            fish.transform.position += -fish.transform.right * i * spacing;
            availableFish.Add(fish);
        }
        RefreshDisplay();
    }

    private void Update()
    {
        if (SelectedFish)
        {
            buyButton.SetActive(true);
            pointer.SetActive(true);
            pointer.transform.position = SelectedFish.transform.position + Vector3.up / 2;
        }
        else
        {
            buyButton.SetActive(false);
            pointer.SetActive(false);
        }
    }

    public void Buy(Hand hand)
    {
        double cost = (SelectedFish as IMarketable).BaseCost();
        if (cost <= bank.jerryBucks)
        {
            bank.jerryBucks -= cost;
            SelectedFish.transform.position = hand.transform.position;
            hand.AttachObject(SelectedFish.gameObject, hand.GetBestGrabbingType(), attachmentFlags);
            availableFish.Remove(SelectedFish);
            if (index > 0) index--;
            RefreshDisplay();
        }
        else
        {
            cantBuyText.SetActive(true);
        }
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
