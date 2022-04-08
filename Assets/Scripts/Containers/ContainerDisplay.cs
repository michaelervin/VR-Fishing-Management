using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ContainerDisplay<T> : MonoBehaviour where T : MonoBehaviour, IContainable, IDisplayable
{
    [SerializeField] MonoBehaviour containerScript;
    [SerializeField] float spacing;
    [SerializeField] Hand hand;

    private static ContainerElementDisplay _displayInfoPrefab;
    private static ContainerElementDisplay DisplayInfoPrefab
    {
        get
        {
            if(_displayInfoPrefab == null)
            {
                _displayInfoPrefab = Resources.Load<ContainerElementDisplay>("Prefabs/UI/BaseContainerElementDisplay");
            }
            return _displayInfoPrefab;
        }
    }

    protected ObjectContainer<T> container;

    private List<ContainerElementDisplay> elements = new List<ContainerElementDisplay>();

    private void Awake()
    {
        Debug.Assert(containerScript is ObjectContainer<T>);
        container = containerScript as ObjectContainer<T>;
    }

    void Start()
    {
        container.onAdd += OnContainerUpdate;
        container.onRemove += OnContainerUpdate;
    }

    void OnContainerUpdate(T f)
    {
        DisplayObjects();
    }

    public void DisplayObjects()
    {
        int i = 0;
        // TODO: don't destroy and reinstantiate all objects
        foreach(var e in elements)
        {
            Destroy(e.gameObject);
        }
        elements.Clear();

        foreach (T o in container.objects)
        {
            DisplayInfo info = o.GetDisplayInfo();
            ContainerElementDisplay element = Instantiate(DisplayInfoPrefab);
            element.transform.SetParent(transform, false);
            element.transform.localPosition += new Vector3(0, i * spacing, 0);
            element.Text = info.text;
            element.SpriteImage = info.image;
            element.referenceObject = o.gameObject;
            element.hand = hand;
            elements.Add(element);

            i++;
        }
    }
}

public interface IDisplayable
{
    public DisplayInfo GetDisplayInfo();
}

public struct DisplayInfo
{
    public string text;
    public Sprite image;
}

