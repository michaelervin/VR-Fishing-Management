using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Hook : MonoBehaviour
{
    public Rigidbody bobber;

    public AudioClip fishingBellSound;

    List<IAttachable> attachedObjects = new List<IAttachable>();
    FishTarget lure = null;
    GameObject unattachableTemp;

    GameObject _visualObject;
    bool Visable
    {
        set => _visualObject.SetActive(value);
    }

    private void Awake()
    {
        _visualObject = transform.GetChild(0).gameObject;
    }

    private void Attach(IAttachable attachable)
    {
        attachedObjects.Add(attachable);
        attachable.Attach(this);
        if(lure) lure.EnableCollider(false);
    }

    public void Detach(IAttachable attachable)
    {
        attachable.Detach();
        attachedObjects.Remove(attachable);
        unattachableTemp = (attachable as MonoBehaviour).gameObject;
        StartCoroutine(CooldownUnattachableTemp());
        if (lure && attachedObjects.Count == 0) lure.EnableCollider(true);
    }

    private void AddLure(FishTarget fishTarget)
    {
        lure = fishTarget;
        fishTarget.AttachHook(this);
        if (fishTarget.staticData.hideHook)
        {
            Visable = false;
        }
    }

    public void RemoveLure()
    {
        lure.DetachHook();
        unattachableTemp = lure.gameObject;
        lure = null;
        Visable = true;
        StartCoroutine(CooldownUnattachableTemp());
    }

    private IEnumerator CooldownUnattachableTemp()
    {
        yield return new WaitForSeconds(3);
        unattachableTemp = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        IAttachable attachable = other.GetComponent<IAttachable>();
        if (attachable != null && other.gameObject != unattachableTemp && !attachedObjects.Contains(attachable))
        {
            Attach(attachable);
        }

        FishTarget fishTarget = other.GetComponent<FishTarget>();
        if (fishTarget != null && other.gameObject != unattachableTemp && lure == null)
        {
            AddLure(fishTarget);
        }
    }
}

public interface IAttachable
{
    public void Attach(Hook hook);
    public void Detach();
}
