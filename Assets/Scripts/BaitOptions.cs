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
                    buyButton.SetActive(true);
                }
                if(jerryBucksAmount.jerryBucks < baitValues[luresListIndex].value)
                {
                    buyButton.SetActive(false);
                }
                if (baitAmounts[luresListIndex].value > 0)
                {
                    sellButton.SetActive(true);
                }
                if (baitAmounts[luresListIndex].value == 0)
                {
                    sellButton.SetActive(false);
                }
                if (luresListIndex > 0)
                {
                    leftButton.SetActive(true);
                }
                if (luresListIndex == 0)
                {
                    leftButton.SetActive(false);
                }
                if (luresListIndex < luresList.Count)
                {
                    rightButton.SetActive(true);
                }
                if (luresListIndex == 2)
                {
                    rightButton.SetActive(false);
                }
            }
            else
            {
                luresList[i].SetActive(false);
            }
        }
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