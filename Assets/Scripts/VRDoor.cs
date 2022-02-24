using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VRDoor : MonoBehaviour
{
    public Object targetScene;

    public void OnUseDoor()
    {
        SceneManager.LoadScene(targetScene.name);
    }
}
