using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bait;

    public int baitsAvailable = 2;

    private void Start()
    {
        Instantiate(bait).transform.position = transform.position;
        
    }
    public void Spawn()
    {
        if (baitsAvailable != 0)
        {
            Instantiate(bait);
            baitsAvailable = baitsAvailable -= 1;
        }
    }
}
