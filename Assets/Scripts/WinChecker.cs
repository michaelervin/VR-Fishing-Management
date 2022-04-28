using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WinChecker : MonoBehaviour
{
    [SerializeField] FishContainer container;
    [SerializeField] string[] requiredFish;
    [SerializeField] GameObject[] enabledObjects;
    [SerializeField] AudioClip winSound;

    private bool won = false;

    private void Start()
    {
        SetActiveObjects(false);
        container.onAdd += CheckContainer;
        container.onRemove += ResetDisplay;
    }

    private void SetActiveObjects(bool active)
    {
        foreach (var o in enabledObjects)
        {
            o.SetActive(active);
        }
    }

    private void ResetDisplay(Fish fish)
    {
        SetActiveObjects(false);
    }

    private void CheckContainer(Fish obj)
    {
        if (won) return;
        foreach (string fish in requiredFish)
        {
            if(!(obj.data.name == fish) && !(container.objects.Find(f => f.data.name == fish) != null))
            {
                SetActiveObjects(false);
                return;
            }
        }
        won = true;
        SetActiveObjects(true);
        AudioSource.PlayClipAtPoint(winSound, transform.position);
    }
}
