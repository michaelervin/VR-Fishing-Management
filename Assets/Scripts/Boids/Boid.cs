using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

    BoidSettings settings;

    // State
    [HideInInspector]
    public Vector3 position;
    [HideInInspector]
    public Vector3 forward;
    Vector3 velocity;

    // To update:
    Vector3 acceleration;
    [HideInInspector]
    public Vector3 avgFlockHeading;
    [HideInInspector]
    public Vector3 avgAvoidanceHeading;
    [HideInInspector]
    public Vector3 centreOfFlockmates;
    [HideInInspector]
    public int numPerceivedFlockmates;

    // Cached
    Material material;
    Transform cachedTransform;
    List<FishTarget> targets;

    public List<string> pursuedTargetTypes;

    Rigidbody rb;

    void Awake () {
        material = transform.GetComponentInChildren<MeshRenderer> ().material;
        cachedTransform = new GameObject("Heading").transform;
        cachedTransform.position = transform.position;
        rb = GetComponent<Rigidbody>();
        targets = new List<FishTarget>();
    }

    private void OnDestroy()
    {
        if(cachedTransform != null)
        {
            Destroy(cachedTransform.gameObject);
        }
    }

    public void Initialize (BoidSettings settings) {
        this.settings = settings;

        position = cachedTransform.position;
        forward = cachedTransform.forward;

        float startSpeed = (settings.minSpeed + settings.maxSpeed) / 2;
        velocity = transform.forward * startSpeed;
    }

    public void AddTarget (FishTarget target)
    {
        if (pursuedTargetTypes.Contains(target.data.type))
        {
            targets.Add(target);
        }
    }

    public void AddTargets (IEnumerable<FishTarget> targets)
    {
        foreach (FishTarget target in targets)
        {
            AddTarget(target);
        }
    }

    public void RemoveTarget (FishTarget target)
    {
        if (pursuedTargetTypes.Contains(target.data.type))
        {
            targets.Remove(target);
        }
    }

    public void SetColour (Color col) {
        if (material != null) {
            material.color = col;
        }
    }

    private void Update()
    {
        if (rb == null)
        {
            transform.position = cachedTransform.position;
            transform.rotation = cachedTransform.rotation;
        }
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            rb.MovePosition(cachedTransform.position);
            rb.MoveRotation(cachedTransform.rotation);
        }
    }

    public void UpdateBoid () {
        Vector3 acceleration = Vector3.zero;

        Vector3 closestTargetOffset = Vector3.one * settings.targetRadius;
        foreach(FishTarget target in targets)
        {
            Vector3 offsetToTarget = (target.transform.position - position);
            if (offsetToTarget.sqrMagnitude < closestTargetOffset.sqrMagnitude)
                closestTargetOffset = offsetToTarget;
        }

        if(closestTargetOffset.sqrMagnitude < settings.targetRadius * settings.targetRadius)
        {
            acceleration = SteerTowards(closestTargetOffset) * settings.targetWeight;
        }

        if (numPerceivedFlockmates != 0) {
            centreOfFlockmates /= numPerceivedFlockmates;

            Vector3 offsetToFlockmatesCentre = (centreOfFlockmates - position);

            var alignmentForce = SteerTowards (avgFlockHeading) * settings.alignWeight;
            var cohesionForce = SteerTowards (offsetToFlockmatesCentre) * settings.cohesionWeight;
            var seperationForce = SteerTowards (avgAvoidanceHeading) * settings.seperateWeight;

            acceleration += alignmentForce;
            acceleration += cohesionForce;
            acceleration += seperationForce;
        }

        if (IsHeadingForCollision ()) {
            Vector3 collisionAvoidDir = ObstacleRays ();
            Vector3 collisionAvoidForce = SteerTowards (collisionAvoidDir) * settings.avoidCollisionWeight;
            acceleration += collisionAvoidForce;
        }

        velocity += acceleration * Time.deltaTime;
        float speed = velocity.magnitude;
        Vector3 dir = velocity / speed;
        speed = Mathf.Clamp (speed, settings.minSpeed, settings.maxSpeed);
        velocity = dir * speed;

        cachedTransform.position += velocity * Time.deltaTime;
        cachedTransform.forward = dir;
        position = cachedTransform.position;
        forward = dir;
    }

    bool IsHeadingForCollision () {
        RaycastHit hit;
        if (Physics.SphereCast (position, settings.boundsRadius, forward, out hit, settings.collisionAvoidDst, settings.obstacleMask)) {
            return true;
        } else { }
        return false;
    }

    Vector3 ObstacleRays () {
        Vector3[] rayDirections = BoidHelper.directions;

        for (int i = 0; i < rayDirections.Length; i++) {
            Vector3 dir = cachedTransform.TransformDirection (rayDirections[i]);
            Ray ray = new Ray (position, dir);
            if (!Physics.SphereCast (ray, settings.boundsRadius, settings.collisionAvoidDst, settings.obstacleMask)) {
                return dir;
            }
        }

        return forward;
    }

    Vector3 SteerTowards (Vector3 vector) {
        Vector3 v = vector.normalized * settings.maxSpeed - velocity;
        return Vector3.ClampMagnitude (v, settings.maxSteerForce);
    }

}