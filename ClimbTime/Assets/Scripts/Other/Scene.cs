using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public string _sceneName = string.Empty;

    public void OnButtonPressed()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
