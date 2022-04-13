using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Hook : MonoBehaviour
{
    public Rigidbody bobber;

    public AudioClip fishingBellSound;

    List<IAttachable> attachedObjects = new List<IAttachable>();
    IAttachable unattachableTemp;

    public void Attach(IAttachable attachable)
    {
        attachedObjects.Add(attachable);
        attachable.Attach(this);
    }

    public void Detach(IAttachable attachable)
    {
        attachable.Detach();
        attachedObjects.Remove(attachable);
        unattachableTemp = attachable;
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
        if (attachable != null && attachable != unattachableTemp && !attachedObjects.Contains(attachable))
        {
            Attach(attachable);
        }
    }
}

public interface IAttachable
{
    public void Attach(Hook hook);
    public void Detach();
}
