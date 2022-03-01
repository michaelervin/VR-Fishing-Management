using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour {

    public enum GizmoType { Never, SelectedOnly, Always }

    public BoidManager boidManager;
    public Boid prefab;
    public Color colour;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBoid();
        }
    }

    private void SpawnBoid()
    {
        Boid boid = Instantiate(prefab, boidManager.transform);
        boid.transform.position = transform.position;
        boid.transform.forward = Random.insideUnitSphere;

        boid.SetColour(colour);
        boidManager.RegisterBoid(boid);
    }

}