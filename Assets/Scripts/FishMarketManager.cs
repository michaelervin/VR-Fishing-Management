using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishMarketManager : MonoBehaviour
{
    public JerryBank jerryBucksAmount;

    List<FishStaticData> fishList = new List<FishStaticData>();

    [SerializeField] Transform screenTransform;
    [SerializeField] Transform spawnPoint;
    List<GameObject> imageObjects = new List<GameObject>();

    [SerializeField] GameObject buyButton;
    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject rightButton;
    [SerializeField] GameObject bankEmptyText;


    public int fishListIndex = 0;

    private void Start()
    {
        fishList.AddRange(FishSpawnerUtility.GetAllStaticData());

        CreateImages();
    }

    private void CreateImages()
    {
        foreach (FishStaticData fish in fishList)
        {
            SpriteRenderer sprite = new GameObject(fish.name).AddComponent<SpriteRenderer>();
            sprite.sprite = fish.sprite;
            sprite.transform.SetParent(screenTransform, false);

            TextMeshPro text = new GameObject("Text").AddComponent<TextMeshPro>();
            text.text = $"Name: {fish.name}\nCost: {fish.cost}";
            text.rectTransform.sizeDelta = new Vector2(1, 1);
            text.fontSize = 1;
            text.transform.SetParent(sprite.transform, false);

            imageObjects.Add(sprite.gameObject);
        }
    }

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
                imageObjects[i].SetActive(true);

                bankEmptyText.SetActive(jerryBucksAmount.jerryBucks < fishList[fishListIndex].cost);
                buyButton.SetActive(jerryBucksAmount.jerryBucks >= fishList[fishListIndex].cost);
                leftButton.SetActive(fishListIndex > 0);
                rightButton.SetActive(fishListIndex < fishList.Count - 1);
            }
            else
            {
                imageObjects[i].SetActive(false);
            }
        }
    }

    public void Purchasing()
    {
        if (jerryBucksAmount.jerryBucks >= fishList[fishListIndex].cost)
        {
            jerryBucksAmount.jerryBucks -= fishList[fishListIndex].cost;
            FishData fishData = new FishData();
            fishData.name = fishList[fishListIndex].name;
            fishData.nickName = "Bubbles";
            fishData.size = 1;
            Fish fish = FishSpawnerUtility.CreateFish(fishData);
            fish.transform.position = spawnPoint.position;
        }
    }

    public void Selling(Fish fish)
    {
        jerryBucksAmount.jerryBucks += fish.staticData.cost;
        fish._hand?.DetachObject(fish.gameObject);
        Destroy(fish.gameObject);
    }
}