using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bait;

    public int baitsAvailable = 2;

    public GameObject Spawn()
    {
        if (baitsAvailable != 0)
        {
            var spawnedBait =  Instantiate(bait);
            baitsAvailable = baitsAvailable -= 1;
            bait.transform.position = transform.position;
            return spawnedBait;
        }
        return null;
    }
}
