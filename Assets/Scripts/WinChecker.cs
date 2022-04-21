using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WinChecker : MonoBehaviour
{
    [SerializeField] FishContainer container;
    [SerializeField] string[] requiredFish;
    [SerializeField] GameObject winText;

    private void Start()
    {
        winText.SetActive(false);
        container.onAdd += CheckContainer;
    }

    private void CheckContainer(Fish obj)
    {
        foreach (string fish in requiredFish)
        {
            if(!(obj.data.name == fish) && !(container.objects.Find(f => f.data.name == fish) != null))
            {
                winText.SetActive(false);
                return;
            }
        }
        winText.SetActive(true);
    }
}
