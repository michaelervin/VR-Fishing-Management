using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    [SerializeField] Transform tip;
    [SerializeField] Rigidbody bobber;
    [SerializeField] Hook hook;
    [SerializeField] float castForce = 500;
    [SerializeField] float reelTime = 1;

    bool isReeling;
    bool isReeled;

    void Start()
    {
        bobber.useGravity = false;
        isReeling = false;
        isReeled = true;
    }

    public void SetBait()
    {
        if (isReeled)
        {
            hook.AddBait();
        }
        else
        {
            Debug.LogWarning("Tried to set bait while not reeled");
        }
    }

    public void LaunchBobber()
    {
        if (!isReeling)
        {
            isReeled = false;
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
        Vector3 originalPosition = bobber.transform.position;

        for (float i = 0; i < reelTime; i += Time.deltaTime)
        {
            bobber.transform.position = Vector3.Lerp(originalPosition, tip.position, i / reelTime);
            yield return null;
        }
        bobber.velocity = Vector3.zero;
        isReeling = false;
        isReeled = true;
    }
}
