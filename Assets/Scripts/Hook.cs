using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Hook : MonoBehaviour, ISavable
{
    public Rigidbody bobber;

    public AudioClip fishingBellSound;

    List<IAttachable> attachedObjects = new List<IAttachable>();
    FishTarget _lure = null;
    FishTarget Lure
    {
        get => _lure;
        set
        {
            _lure = value;
            lureData = _lure ? _lure.data : null;
        }
    }
    public FishTargetData lureData;
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
        if(Lure) Lure.EnableCollider(false);
    }

    public void Detach(IAttachable attachable)
    {
        attachable.Detach();
        attachedObjects.Remove(attachable);
        unattachableTemp = (attachable as MonoBehaviour).gameObject;
        StartCoroutine(CooldownUnattachableTemp());
        if (Lure && attachedObjects.Count == 0) Lure.EnableCollider(true);
    }

    private void AddLure(FishTarget fishTarget)
    {
        Lure = fishTarget;
        fishTarget.AttachHook(this);
        if (fishTarget.staticData.hideHook)
        {
            Visable = false;
        }
    }

    public void RemoveLure()
    {
        Lure.DetachHook();
        unattachableTemp = Lure.gameObject;
        Lure = null;
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
        if (fishTarget != null && other.gameObject != unattachableTemp && Lure == null)
        {
            AddLure(fishTarget);
        }
    }

    Type ISavable.GetSaveDataType()
    {
        return typeof(HookSaveData);
    }

    void ISavable.OnFinishLoad()
    {
        Lure = FishTargetSpawnerUtility.CreateTarget(lureData);
    }

    class HookSaveData : SaveData
    {
        public FishTargetData lureData;
    }
}

public interface IAttachable
{
    public void Attach(Hook hook);
    public void Detach();
}
