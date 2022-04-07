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
        T containable = other.GetComponent<T>();
        if (containable != null)
        {
            if (!TryAdd(containable))
            {
                Debug.LogWarning("Container has no space!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
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

