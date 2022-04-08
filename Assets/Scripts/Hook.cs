using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Hook : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] float hookChance = 1;
    [SerializeField] GameObject baitPrefab;
    [SerializeField] Rigidbody bobber;

    public AudioClip fishingBellSound;

    /// <summary>
    /// The bait attatched to the hook. If there's no bait, this value will be null.
    /// </summary>
    [SerializeField] GameObject attachedBait;
    /// <summary>
    /// the Fish Attatched to the hook. If there's no fish, this value will be null.
    /// </summary>
    public Fish attachedFish;

    public bool AddBait()
    {
        if(attachedBait == null)
        {
            attachedBait = Instantiate(baitPrefab);
            attachedBait.AddComponent<Follow>().followTransform = transform;
            return true;
        }
        else
        {
            Debug.LogWarning("Tried to add bait when there's already bait");
            return false;
        }
    }

    public bool AddBait(FishTarget bait)
    {
        if (attachedBait == null)
        {
            attachedBait = bait.gameObject;
            bait.gameObject.AddComponent<Follow>().followTransform = transform;
            return true;
        }
        else
        {
            Debug.LogWarning("Tried to add bait when there's already bait");
            return false;
        }
    }

    public void DetachFish()
    {
        attachedFish.DetachHook();
        attachedFish = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == attachedFish?.gameObject) return;
        if (other.gameObject == attachedBait?.gameObject) return;

        Fish fish = other.GetComponent<Fish>();
        if (fish != null)
        {
            if (Random.Range(0f, 1f) < hookChance)
            {
                if (fish.AttachHook(this))
                {
                    attachedFish = fish;
                    StartCoroutine(fish.TugBobber(bobber));
                    AudioSource.PlayClipAtPoint(fishingBellSound, transform.position);
                }
            }
        }

        FishTarget bait = other.GetComponent<FishTarget>();
        if (bait != null)
        {
            AddBait(bait);
        }
    }
}
