using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class ContainerElementDisplay : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text text;
    [SerializeField] UIElement uiElement;

    public Sprite SpriteImage
    {
        set => image.sprite = value;
    }

    public string Text
    {
        set => text.text = value;
    }

    public void Click()
    {
        Debug.Log(text.text);
    }
}
