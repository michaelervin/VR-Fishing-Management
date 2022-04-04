using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDataScreenManager : MonoBehaviour
{
    [SerializeField] List<GameObject> fishScreensList;
    public int fishScreenListIndex = 0;

    // Changes the Fish Data Screen based on fishScreensList index
    public void IncreaseFishListIndex()
    {
        if (fishScreenListIndex < 3)
        {
            fishScreenListIndex++;
        }
    }

    public void DecreaseFishLIstIndex()
    {
        if (fishScreenListIndex > 0)
        {
            fishScreenListIndex--;
        }
    }

    private void Update()
    {
        for(int i = 0; i < fishScreensList.Count; i++)
        {
            if(i == fishScreenListIndex)
            {
                fishScreensList[i].SetActive(true);
            }
            else
            {
                fishScreensList[i].SetActive(false);
            }
        }
    }
}