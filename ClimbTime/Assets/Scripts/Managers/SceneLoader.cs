using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject plr;
    public GameManager gm;
    public GameObject spawn;

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        DontDestroyOnLoad(this);
    }

    public void PlayGame()
    {
        gm.gameState = GameManager.GameStates.arena;
        SceneManager.LoadScene("Arena");
        //spawn = GameObject.FindGameObjectWithTag("PlayerSpawn");
        Instantiate(plr, spawn.transform.position, Quaternion.identity);
    }

    public void ArenaEnd()
    {
        gm.gameState = GameManager.GameStates.shopping;
        SceneManager.LoadScene("Shopping");
        spawn = GameObject.FindGameObjectWithTag("PlayerSpawn");
        Instantiate(plr, spawn.transform.position, Quaternion.identity);
    }

    public void MainMenu()
    {
        gm.gameState = GameManager.GameStates.init;
        SceneManager.LoadScene("MainMenu");
    }

    public void Mountain()
    {
        gm.gameState = GameManager.GameStates.mountain;
        SceneManager.LoadScene("Mountain");
        spawn = GameObject.FindGameObjectWithTag("PlayerSpawn");
        Instantiate(plr, spawn.transform.position, Quaternion.identity);
    }
}
