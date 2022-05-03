using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishDataScreenManager : MonoBehaviour
{
    [SerializeField] List<FishStaticData> fishScreensList;
    [SerializeField] Image image;
    [SerializeField] Text text;
    [SerializeField] Text eatsText;
    public int fishScreenListIndex = 0;

    void Start()
    {
        UpdateSign();
    }

    // Changes the Fish Data Screen based on fishScreensList index
    public void IncreaseFishListIndex()
    {
        if (fishScreenListIndex < fishScreensList.Count - 1)
        {
            fishScreenListIndex++;
        }
        UpdateSign();
    }

    public void DecreaseFishLIstIndex()
    {
        if (fishScreenListIndex > 0)
        {
            fishScreenListIndex--;
        }
        UpdateSign();
    }

    private void UpdateSign()
    {
        image.sprite = fishScreensList[fishScreenListIndex].sprite;
        text.text = fishScreensList[fishScreenListIndex].description;
        eatsText.text = "Eats:\n";
        foreach (var target in fishScreensList[fishScreenListIndex].targetTypes)
        {
            eatsText.text += target + "\n";
        }
    }
}