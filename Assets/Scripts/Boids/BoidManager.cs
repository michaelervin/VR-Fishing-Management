using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour {

    const int threadGroupSize = 1024;

    public BoidSettings settings;
    public ComputeShader compute;
    List<FishTarget> targets;
    List<Boid> boids;

    public void Awake()
    {
        targets = new List<FishTarget>();
        boids = new List<Boid>();
    }

    public void RegisterBoid(Boid b)
    {
        boids.Add(b);
        b.Initialize(settings);
        b.AddTargets(targets);
    }

    public void UnregisterBoid(Boid boid)
    {
        boids.Remove(boid);
    }

    public void RegisterTarget(FishTarget target)
    {
        foreach(Boid b in boids)
        {
            b.AddTarget(target);
        }
        targets.Add(target);
    }

    public void UnregisterTarget(FishTarget target)
    {
        foreach (Boid b in boids)
        {
            b.RemoveTarget(target);
        }
        targets.Remove(target);
    }

    void Update ()
    {
        if (boids == null || boids.Count == 0)
        {
            return;
        }

        int numBoids = boids.Count;
        var boidData = new BoidData[numBoids];

        for (int i = 0; i < boids.Count; i++)
        {
            boidData[i].position = boids[i].position;
            boidData[i].direction = boids[i].forward;
        }

        var boidBuffer = new ComputeBuffer(numBoids, BoidData.Size);
        boidBuffer.SetData(boidData);

        compute.SetBuffer(0, "boids", boidBuffer);
        compute.SetInt("numBoids", boids.Count);
        compute.SetFloat("viewRadius", settings.perceptionRadius);
        compute.SetFloat("avoidRadius", settings.avoidanceRadius);

        int threadGroups = Mathf.CeilToInt(numBoids / (float)threadGroupSize);
        compute.Dispatch(0, threadGroups, 1, 1);

        boidBuffer.GetData(boidData);

        for (int i = 0; i < boids.Count; i++)
        {
            boids[i].avgFlockHeading = boidData[i].flockHeading;
            boids[i].centreOfFlockmates = boidData[i].flockCentre;
            boids[i].avgAvoidanceHeading = boidData[i].avoidanceHeading;
            boids[i].numPerceivedFlockmates = boidData[i].numFlockmates;

            boids[i].UpdateBoid();
        }

        boidBuffer.Release();
    }

    public struct BoidData {
        public Vector3 position;
        public Vector3 direction;

        public Vector3 flockHeading;
        public Vector3 flockCentre;
        public Vector3 avoidanceHeading;
        public int numFlockmates;

        public static int Size {
            get {
                return sizeof (float) * 3 * 5 + sizeof (int);
            }
        }
    }

    public void OnDrawGizmos()
    {
        if(targets != null)
        {
            foreach (FishTarget target in targets)
            {
                Gizmos.DrawWireSphere(target.transform.position, settings.targetRadius);
            }
        }
    }
}