using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoidManager))]
[RequireComponent(typeof(Collider))]
public class BoidFishContainer : FishContainer
{
    BoidManager boidManager;
    Collider attatchedCollider;

    Dictionary<Fish, Boid> boidDict;

    protected override void Awake()
    {
        base.Awake();
        boidManager = GetComponent<BoidManager>();
        attatchedCollider = GetComponent<Collider>();
        boidDict = new Dictionary<Fish, Boid>();
    }

    public override void Add(Fish fish)
    {
        base.Add(fish);
        fish.transform.LookAt(transform);
        // if the fish is completely submerged, randomize the rotation
        if (attatchedCollider.bounds.Contains(fish.transform.position + Vector3.one)) 
        {
            fish.transform.rotation = Random.rotation;
        }
        Boid boid = fish.AddBoidComponent();
        boidManager.RegisterBoid(boid);
        boidDict.Add(fish, boid);
    }

    public override void Remove(Fish fish)
    {
        boidManager.UnregisterBoid(boidDict[fish]);
        Destroy(fish.gameObject.GetComponent<Boid>());
        base.Remove(fish);

        boidDict.Remove(fish);
        fish.transform.parent = null;
    }

    public void Add(Transform target)
    {
        boidManager.RegisterTarget(target);
    }

    public void Remove(Transform target)
    {
        boidManager.UnregisterTarget(target);
    }
}
