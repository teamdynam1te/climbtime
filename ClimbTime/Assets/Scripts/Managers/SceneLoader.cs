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
        gm = GetComponent<GameManager>();

        //DontDestroyOnLoad(this);
    }

    public void PlayGame()
    {       
        SceneManager.LoadScene("Arena");
        StartCoroutine(WaitForPlayerSpawn(0.1f));
        gm.gameState = GameManager.GameStates.arena;
    }    
    IEnumerator WaitForPlayerSpawn(float time)
    {
        yield return new WaitForSeconds(time);
        spawn = GameObject.FindGameObjectWithTag("PlayerSpawn");
        if(spawn != null)
        {
            gm.player = Instantiate(plr, spawn.transform.position, Quaternion.identity).GetComponent<Player>();
        }
        else
        {
            spawn = GameObject.FindGameObjectWithTag("PlayerSpawn");
            gm.player = Instantiate(plr, spawn.transform.position, Quaternion.identity).GetComponent<Player>();
        }
    }

    IEnumerator WaitForScore(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void ArenaEnd()
    {
        SceneManager.LoadScene("Shopping");
        StartCoroutine(WaitForPlayerSpawn(0.1f));
        gm.gameState = GameManager.GameStates.shopping;
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
        gm.gameState = GameManager.GameStates.init;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        //Destroy(plr);
        gm.gameState = GameManager.GameStates.init;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
        StartCoroutine(WaitForScore(0.1f));
        gm.gameState = GameManager.GameStates.end;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameOver();
        }
    }
    public void Mountain()
    {
        SceneManager.LoadScene("Mountain");
        StartCoroutine(WaitForPlayerSpawn(0.1f));
        gm.timeLeft = gm.defaultTime;
        gm.timerActive = false;
        gm.initHeight = gm.player.transform.position.y;
        gm.gameState = GameManager.GameStates.mountain;
    }
}
