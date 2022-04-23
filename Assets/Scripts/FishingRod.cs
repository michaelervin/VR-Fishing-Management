using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class FishingRod : MonoBehaviour
{
    [SerializeField] VelocityEstimator tip;
    [SerializeField] Rigidbody bobber;
    [SerializeField] Hook hook;
    [SerializeField] float reelTime = 1;

    bool isReeling;
    bool isReeled;

    void Start()
    {
        bobber.useGravity = false;
        isReeling = false;
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

    public void LaunchBobber()
    {
        if (!isReeling)
        {
            isReeled = false;
            bobber.useGravity = true;
            bobber.velocity = tip.GetVelocityEstimate();
        }
    }

    public void ReelBobber()
    {
        if (!isReeled)
        {
            StartCoroutine(ReelBobberCoroutine());
        }
    }

    private IEnumerator ReelBobberCoroutine()
    {
        isReeling = true;
        bobber.useGravity = false;
        Vector3 originalPosition = bobber.transform.position;

        for (float i = 0; i < reelTime; i += Time.deltaTime)
        {
            bobber.transform.position = Vector3.Lerp(originalPosition, tip.transform.position, i / reelTime);
            yield return null;
        }
        bobber.velocity = Vector3.zero;
        isReeling = false;
        isReeled = true;
    }
}
