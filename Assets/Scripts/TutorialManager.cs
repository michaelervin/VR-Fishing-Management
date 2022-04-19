using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject tutorialScreens;
    void EnableTutorial()
    {
        tutorialScreens.SetActive(true);
    }

    void DisableTutorial()
    {
        tutorialScreens.SetActive(false);
    }
}
