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
    [SerializeField] AudioClip winSound;

    private void Start()
    {
        winText.SetActive(false);
        container.onAdd += CheckContainer;
        container.onRemove += ResetDisplay;
    }

    private void ResetDisplay(Fish fish)
    {
        winText.SetActive(false);
    }

    private void CheckContainer(Fish obj)
    {
        if (winText.activeInHierarchy) return;
        foreach (string fish in requiredFish)
        {
            if(!(obj.data.name == fish) && !(container.objects.Find(f => f.data.name == fish) != null))
            {
                winText.SetActive(false);
                return;
            }
        }
        winText.SetActive(true);
        AudioSource.PlayClipAtPoint(winSound, winText.transform.position);
    }
}
