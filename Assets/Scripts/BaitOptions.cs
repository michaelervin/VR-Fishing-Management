using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//These classes are used to store the values for each item in the market
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

    public JerryBank jerryBucksAmount;

    [SerializeField] List<GameObject> luresList;

    [SerializeField] GameObject sellButton;
    [SerializeField] GameObject buyButton;
    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject rightButton;


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
                
                if(jerryBucksAmount.jerryBucks >= baitValues[luresListIndex].value)
                {
                    EnableBuyButton();
                }
                if(jerryBucksAmount.jerryBucks < baitValues[luresListIndex].value)
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
                if (luresListIndex > 0)
                {
                    EnableLeftButton();
                }
                if (luresListIndex == 0)
                {
                    DisableLeftButton();
                }
                if (luresListIndex < luresList.Count)
                {
                    EnableRightButton();
                }
                if (luresListIndex == 2)
                {
                    DisableRightButton();
                }
            }
            else
            {
                luresList[i].SetActive(false);
            }
        }
    }

    // These methods manage the buttons
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

    private void EnableLeftButton()
    {
        leftButton.SetActive(true);
    }

    private void DisableLeftButton()
    {
        leftButton.SetActive(false);
    }

    private void EnableRightButton()
    {
        rightButton.SetActive(true);
    }

    private void DisableRightButton()
    {
        rightButton.SetActive(false);
    }

    public void Purchasing()
    {
        if (jerryBucksAmount.jerryBucks >= baitValues[luresListIndex].value)
        {
            jerryBucksAmount.jerryBucks -= baitValues[luresListIndex].value;
        }

        baitAmounts[luresListIndex].value++;
    }

    public void Selling()
    {
        jerryBucksAmount.jerryBucks += baitValues[luresListIndex].value;
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
        public BaitAmount[] baitAmounts;
    }
}