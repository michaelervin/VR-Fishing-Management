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
public class BaitOptions : MonoBehaviour, ISavable
{
    [SerializeField] BaitValue[] baitValues;

    [SerializeField] double jerryBucks;

    [SerializeField] List<GameObject> luresList;

    [SerializeField] GameObject sellButton;
    [SerializeField] GameObject buyButton;


    public int luresListIndex = 0;

    void Start()
    {
        jerryBucks = 10.00;
    }
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

    public void Purchasing()
    {
        if (jerryBucks >= baitValues[luresListIndex].value)
        {
            jerryBucks -= baitValues[luresListIndex].value;
        }
    }

    public void Selling()
    {
        jerryBucks += baitValues[luresListIndex].value;
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
    }
}