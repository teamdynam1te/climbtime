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
            Instantiate(plr, spawn.transform.position, Quaternion.identity);           
        }
        else
        {
            spawn = GameObject.FindGameObjectWithTag("PlayerSpawn");
            Instantiate(plr, spawn.transform.position, Quaternion.identity);
        }
    }

    public void ArenaEnd()
    {
        SceneManager.LoadScene("Shopping");
        StartCoroutine(WaitForPlayerSpawn(0.1f));
        gm.gameState = GameManager.GameStates.shopping;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        //Destroy(plr);
        gm.gameState = GameManager.GameStates.init;
    }

    public void Mountain()
    {
        SceneManager.LoadScene("Mountain");
        StartCoroutine(WaitForPlayerSpawn(0.1f));
        gm.gameState = GameManager.GameStates.mountain;
    }
}
