using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerDisplay<T> : MonoBehaviour where T : MonoBehaviour, IContainable, IDisplayable
{
    [SerializeField] MonoBehaviour containerScript;
    [SerializeField] float spacing;

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

    ObjectContainer<T> container;

    private void Awake()
    {
        Debug.Assert(containerScript is ObjectContainer<T>);
        container = containerScript as ObjectContainer<T>;
    }

    public void DisplayObjects()
    {
        int i = 0;
        foreach (T o in container.objects)
        {
            DisplayInfo info = o.GetDisplayInfo();
            ContainerElementDisplay element = Instantiate(DisplayInfoPrefab);
            element.transform.SetParent(transform, false);
            element.Text = info.text;
            element.SpriteImage = info.image;
            element.transform.position += new Vector3(0, i * spacing, 0);

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

