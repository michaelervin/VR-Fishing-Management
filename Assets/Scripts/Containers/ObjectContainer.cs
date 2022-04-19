using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ObjectContainer<T> : MonoBehaviour where T : MonoBehaviour, IContainable
{
    public float capacity;
    public float usedCapacity;

    public List<T> objects = new List<T>();

    public Action<T> onAdd;
    public Action<T> onRemove;

    public bool HasSpace(T o)
    {
        return usedCapacity + o.RequiredSpace <= capacity;
    }

    public bool TryAdd(T o)
    {
        if (!HasSpace(o))
        {
            return false;
        }
        objects.Add(o);
        o.transform.parent = transform;
        usedCapacity += o.RequiredSpace;
        o.Contain(this);
        onAdd?.Invoke(o);
        return true;
    }

    public void Remove(T o)
    {
        objects.Remove(o);
        o.transform.parent = null;
        usedCapacity -= o.RequiredSpace;
        o.Release();
        onRemove?.Invoke(o);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_ignoredCol.Contains(other)) return;
        T containable = other.GetComponent<T>();
        if (containable != null)
        {
            _ignoredCol.Add(other);
            StartCoroutine(IgnoreNextCollision(other));
            if (!TryAdd(containable))
            {
                Debug.LogWarning("Container has no space!");
            }
        }
    }

    // Avoid triggering twice colliders that change isKinematic on collision TODO: find a better workaround? 
    private List<Collider> _ignoredCol = new List<Collider>();
    private IEnumerator IgnoreNextCollision(Collider other)
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        _ignoredCol.Remove(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (_ignoredCol.Contains(other)) return;
        T containable = other.GetComponent<T>();
        if (containable != null)
        {
            Remove(containable);
        }
    }
}

public interface IContainable
{
    public float RequiredSpace { get; }
    public void Contain<T>(ObjectContainer<T> container) where T : MonoBehaviour, IContainable;
    public void Release();
}

