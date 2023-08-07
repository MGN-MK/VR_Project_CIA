using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public string scene;

    public void ChangeScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
}
