using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleButton : MonoBehaviour
{
    public GameManager gm;
    private void Awake()
    {
        gm = GetComponent<GameManager>();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        //Destroy(plr);
        gm.gameState = GameManager.GameStates.init;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
        gm.gameState = GameManager.GameStates.init;
    }
}
