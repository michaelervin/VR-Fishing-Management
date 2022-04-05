using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    [SerializeField] List<GameObject> luresList;
    
    public int luresListIndex = 0;
    public int currency = 10;
    public int crankBaitValue = 3;
    public int minnowBaitValue = 3;
    public int jerryBaitValue = 4;
    
    // Changes the Fish Data Screen based on fishScreensList index
    public void IncreaseLuresIndex()
    {
        if (luresListIndex < 3)
        {
            luresListIndex++;
        }
    }

    public void DecreaseLuresIndex()
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
            }
            else
            {
                luresList[i].SetActive(false);
            }
        }
    }

    public void Buying()
    {
        if (currency >= luresList[i])
        {
            currency -= luresList[i];
        }
            
    }

    public void Selling()
    {
        currency += luresList[i];
    }
}
