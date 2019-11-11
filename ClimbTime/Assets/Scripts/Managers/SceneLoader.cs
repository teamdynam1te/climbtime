using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Arena");
    }

    public void ArenaEnd()
    {
    new WaitForSeconds(1f);
    SceneManager.LoadScene("Shopping");
    }
}
