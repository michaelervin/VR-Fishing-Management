using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    [SerializeField] Transform tip;
    [SerializeField] Rigidbody bobber;
    [SerializeField] float castForce = 500;
    [SerializeField] float reelTime = 1;

    bool isReeling;

    void Start()
    {
        bobber.useGravity = false;
        isReeling = false;
    }

    public void LaunchBobber()
    {
        if (!isReeling)
        {
            bobber.useGravity = true;
            bobber.AddForce(tip.forward * castForce);
        }
        else
        {
            Debug.LogWarning("Tried to launch the bobber while reeling");
        }
    }

    public IEnumerator ReelBobber()
    {
        isReeling = true;
        bobber.useGravity = false;
        bobber.velocity = Vector3.zero;
        Vector3 originalPosition = bobber.transform.position;

        for (float i = 0; i < reelTime; i += Time.deltaTime)
        {
            bobber.transform.position = Vector3.Lerp(originalPosition, tip.position, i / reelTime);
            yield return null;
        }
        isReeling = false;
    }
}
