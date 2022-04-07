using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//These classes are used to store the values for each item in the market
[Serializable]
public class FishBuyValue
{
    public string name = "Jerry";
    public double value = 0;
}

[Serializable]
public class FishSellValue
{
    public string name = "Jerry";
    public double value = 0;
}

[Serializable]
public class FishAmount
{
    public string name = "Jerry";
    public double value = 0;
}
public class FishMarketManager : MonoBehaviour, ISavable
{
    [SerializeField] FishBuyValue[] fishBuyValues;
    [SerializeField] FishSellValue[] fishSellValues;

    public FishAmount[] fishAmounts;

    public double jerryBucks;

    [SerializeField] List<GameObject> fishList;

    [SerializeField] GameObject sellButton;
    [SerializeField] GameObject buyButton;
    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject rightButton;


    public int fishListIndex = 0;

    // Changes Fish displayed on screen based on fishListIndex
    public void IncreaseIndex()
    {
        if (fishListIndex < 3)
        {
            fishListIndex++;
        }
    }

    public void DecreaseIndex()
    {
        if (fishListIndex > 0)
        {
            fishListIndex--;
        }
    }

    private void Update()
    {
        for (int i = 0; i < fishList.Count; i++)
        {
            if (i == fishListIndex)
            {
                fishList[i].SetActive(true);

                if (jerryBucks >= fishBuyValues[fishListIndex].value)
                {
                    EnableBuyButton();
                }
                if (jerryBucks < fishBuyValues[fishListIndex].value)
                {
                    DisableBuyButton();
                }
                if (fishAmounts[fishListIndex].value > 0)
                {
                    EnableSellButton();
                }
                if (fishAmounts[fishListIndex].value == 0)
                {
                    DisableSellButton();
                }
                if (fishListIndex > 0)
                {
                    EnableLeftButton();
                }
                if (fishListIndex == 0)
                {
                    DisableLeftButton();
                }
                if (fishListIndex < fishList.Count)
                {
                    EnableRightButton();
                }
                if (fishListIndex == 3)
                {
                    DisableRightButton();
                }
            }
            else
            {
                fishList[i].SetActive(false);
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
        if (jerryBucks >= fishBuyValues[fishListIndex].value)
        {
            jerryBucks -= fishBuyValues[fishListIndex].value;
        }

        fishAmounts[fishListIndex].value++;
    }

    public void Selling()
    {
        jerryBucks += fishSellValues[fishListIndex].value;
        fishAmounts[fishListIndex].value--;
    }

    Type ISavable.GetSaveDataType()
    {
        return typeof(FishMarketSaveData);
    }

    void ISavable.OnFinishLoad()
    {

    }

    class FishMarketSaveData : SaveData
    {
        public double jerryBucks;
        public FishAmount[] fishAmounts;
    }
}