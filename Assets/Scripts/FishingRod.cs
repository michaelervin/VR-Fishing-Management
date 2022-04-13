using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    [SerializeField] VelocityTracker tip;
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
            bobber.velocity = tip.velocity;
        }
        else
        {
            Debug.LogWarning("Tried to launch the bobber while reeling");
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
