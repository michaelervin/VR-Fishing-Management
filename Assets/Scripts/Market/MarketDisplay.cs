using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketDisplay : MonoBehaviour
{
    [SerializeField] Text displayName;
    [SerializeField] Text cost;
    [SerializeField] Image image;

    public void SetDisplay(IDisplayable displayable)
    {
        if (displayable != null)
        {
            DisplayInfo info = displayable.GetDisplayInfo();
            displayName.text = info.title;
            cost.text = $"${info.cost}";
            image.sprite = info.sprite;
        }
        else
        {
            displayName.text = "Select and item";
            cost.text = $"";
            image.sprite = null;
        }
    }
}
