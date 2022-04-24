using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class FishingRod : MonoBehaviour
{
    [SerializeField] VelocityEstimator tip;
    [SerializeField] Rigidbody bobber;
    [SerializeField] Hook hook;
    [SerializeField] float reelSpeed = 0.01f;

    bool isReeled;

    void Start()
    {
        bobber.useGravity = false;
        isReeled = true;
        tip.BeginEstimatingVelocity();
    }

    private void FixedUpdate()
    {
        if (isReeled)
        {
            bobber.MovePosition(tip.transform.position);
        }
    }

    public void ReleaseBobber()
    {
        if (isReeled)
        {
            LaunchBobber();
        }
        else
        {
            StopReeling();
        }
    }

    private void LaunchBobber()
    {
        isReeled = false;
        bobber.useGravity = true;
        bobber.velocity = tip.GetVelocityEstimate();
    }

    public void ReelBobber()
    {
        if (!isReeled)
        {
            bobber.useGravity = false;
            bobber.MovePosition(Vector3.MoveTowards(bobber.position, tip.transform.position, reelSpeed));
        }
    }

    private void StopReeling()
    {
        if ((bobber.position - tip.transform.position).sqrMagnitude < 1)
        {
            isReeled = true;
        }
        else
        {
            bobber.useGravity = true;
        }
    }
}
