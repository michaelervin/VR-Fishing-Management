using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject tutorialScreens;
    [SerializeField] TextMeshPro text;

    public bool isPressed;

    private void Start()
    {
        text.text = "Tutorial Disabled";
    }

    public void Manager()
    {
        isPressed = !isPressed;

        if (isPressed == true)
        {
            text.text = "Tutorial Enabled";
            tutorialScreens.SetActive(true);
        }
        else
        {
            text.text = "Tutorial Disabled";
            tutorialScreens.SetActive(false);
        }
    }
}