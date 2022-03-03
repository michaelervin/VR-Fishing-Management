using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoidManager))]
public class BoidFishContainer : FishContainer
{
    BoidManager boidManager;

    Dictionary<Fish, Boid> boidDict;

    private void Awake()
    {
        boidManager = GetComponent<BoidManager>();
        boidDict = new Dictionary<Fish, Boid>();
    }

    public override void Add(Fish fish)
    {
        base.Add(fish);
        fish.transform.parent = transform;
        fish.transform.localPosition = Vector3.zero;
        Boid boid = fish.gameObject.AddComponent<Boid>();
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
