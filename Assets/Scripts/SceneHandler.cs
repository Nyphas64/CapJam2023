using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField]
    string scene;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
