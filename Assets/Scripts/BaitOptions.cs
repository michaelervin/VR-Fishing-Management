using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaitOptions : MonoBehaviour
{
    public JerryBank jerryBucksAmount;

    List<FishTargetStaticData> luresList = new List<FishTargetStaticData>();

    [SerializeField] Transform screenTransform;
    [SerializeField] Transform spawnPoint;
    List<GameObject> imageObjects = new List<GameObject>();

    [SerializeField] GameObject emptyBankText;
    [SerializeField] GameObject buyButton;
    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject rightButton;


    public int luresListIndex = 0;

    private void Start()
    {
        luresList.AddRange(FishTargetSpawnerUtility.GetAllStaticData());

        CreateImages();
    }

    private void CreateImages()
    {
        foreach (FishTargetStaticData bait in luresList)
        {
            SpriteRenderer sprite = new GameObject(bait.name).AddComponent<SpriteRenderer>();
            sprite.sprite = bait.sprite;
            sprite.transform.SetParent(screenTransform, false);

            TextMeshPro text = new GameObject("Text").AddComponent<TextMeshPro>();
            text.text = $"Name: {bait.name}\nCost: {bait.cost}";
            text.rectTransform.sizeDelta = new Vector2(1, 1);
            text.fontSize = 1;
            text.transform.SetParent(sprite.transform, false);

            imageObjects.Add(sprite.gameObject);
        }
    }

    // Changes Lure displaye on screen based on luresListIndex
    public void IncreaseIndex()
    {
        if (luresListIndex < luresList.Count - 1)
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
                imageObjects[i].SetActive(true);

                emptyBankText.SetActive(jerryBucksAmount.jerryBucks < luresList[luresListIndex].cost);
                buyButton.SetActive(jerryBucksAmount.jerryBucks >= luresList[luresListIndex].cost);
                leftButton.SetActive(luresListIndex > 0);
                rightButton.SetActive(luresListIndex < luresList.Count - 1);
            }
            else
            {
                imageObjects[i].SetActive(false);
            }
        }
    }

    public void Purchasing()
    {
        if (jerryBucksAmount.jerryBucks >= luresList[luresListIndex].cost)
        {
            jerryBucksAmount.jerryBucks -= luresList[luresListIndex].cost;
            FishTargetData data = new FishTargetData();
            data.type = luresList[luresListIndex].name;
            FishTarget target = FishTargetSpawnerUtility.CreateTarget(data);
            target.transform.position = spawnPoint.position;
        }
    }

    public void Selling(FishTarget target)
    {
        jerryBucksAmount.jerryBucks += target.staticData.cost;
        target._hand?.DetachObject(target.gameObject);
        Destroy(target.gameObject);
    }
}