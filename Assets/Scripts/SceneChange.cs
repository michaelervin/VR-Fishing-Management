using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public Object targetScene;

    public void OnUseDoor()
    {
        SceneManager.LoadScene(targetScene.name);
    }
}
