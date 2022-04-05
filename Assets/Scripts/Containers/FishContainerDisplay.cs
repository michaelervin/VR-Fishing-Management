using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishContainerDisplay : ContainerDisplay<Fish>
{
    void Start()
    {
        container.onAdd += OnContainerUpdate;
        container.onRemove += OnContainerUpdate;
    }

    void OnContainerUpdate(Fish f)
    {
        DisplayObjects();
    }
}
