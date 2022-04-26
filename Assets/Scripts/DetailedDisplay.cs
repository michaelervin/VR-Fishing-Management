using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailedDisplay : MonoBehaviour
{
    [SerializeField] Text title;
    [SerializeField] Text subTitle;
    [SerializeField] Text description;
    [SerializeField] Image image;

    private void Start()
    {
        SetDisplay(null);
    }

    public void SetDisplay(IDisplayable displayable)
    {
        if (displayable != null)
        {
            DisplayInfo info = displayable.GetDisplayInfo();
            title.text = info.title;
            subTitle.text = info.subTitle;
            description.text = info.description;
            image.enabled = true;
            image.sprite = info.sprite;
        }
        else
        {
            title.text = "";
            subTitle.text = "";
            description.text = "";
            image.enabled = false;
        }
    }
}
