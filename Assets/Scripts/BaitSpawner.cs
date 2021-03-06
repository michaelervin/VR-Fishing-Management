using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitSpawner : MonoBehaviour
{
    [SerializeField] private FishTarget bait;

    public FishTarget Spawn()
    {
        var spawnedBait = Instantiate(bait);
        bait.transform.position = transform.position;
        return spawnedBait;
    }
}
