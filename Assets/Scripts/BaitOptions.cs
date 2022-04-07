using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BaitValue
{
    public string name = "Jerry";
    public double value = 0;
}

[Serializable]
public class BaitAmount
{
    public string name = "Jerry";
    public double value = 0;
}
public class BaitOptions : MonoBehaviour, ISavable
{
    [SerializeField] BaitValue[] baitValues;

    public BaitAmount[] baitAmounts;

    public double jerryBucks;

    [SerializeField] List<GameObject> luresList;

    [SerializeField] GameObject sellButton;
    [SerializeField] GameObject buyButton;


    public int luresListIndex = 0;

    // Changes Lure displaye on screen based on luresListIndex
    public void IncreaseIndex()
    {
        if (luresListIndex < 2)
        {
            luresListIndex++;
        }
    }

    public void DecreaseIndex()
    {
        if (luresListIndex > 0)
        {
            luresListIndex--;
        }
    }

    private void Update()
    {
        for (int i = 0; i < luresList.Count; i++)
        {
            if (i == luresListIndex)
            {
                luresList[i].SetActive(true);
                
                if(jerryBucks >= baitValues[luresListIndex].value)
                {
                    EnableBuyButton();
                }
                if(jerryBucks < baitValues[luresListIndex].value)
                {
                    DisableBuyButton();
                }
                if (baitAmounts[luresListIndex].value > 0)
                {
                    EnableSellButton();
                }
                if (baitAmounts[luresListIndex].value == 0)
                {
                    DisableSellButton();
                }
            }
            else
            {
                luresList[i].SetActive(false);
            }
        }
    }

    private void EnableBuyButton()
    {
        buyButton.SetActive(true);
    }

    private void DisableBuyButton()
    {
        buyButton.SetActive(false);
    }

    private void EnableSellButton()
    {
        sellButton.SetActive(true);
    }

    private void DisableSellButton()
    {
        sellButton.SetActive(false);
    }

    public void Purchasing()
    {
        if (jerryBucks >= baitValues[luresListIndex].value)
        {
            jerryBucks -= baitValues[luresListIndex].value;
        }

        baitAmounts[luresListIndex].value++;
    }

    public void Selling()
    {
        jerryBucks += baitValues[luresListIndex].value;
        baitAmounts[luresListIndex].value--;
    }

    Type ISavable.GetSaveDataType()
    {
        return typeof(BaitOptionsSaveData);
    }

    void ISavable.OnFinishLoad()
    {

    }

    class BaitOptionsSaveData: SaveData
    {
        public double jerryBucks;
        public BaitAmount[] baitAmounts;
    }
}