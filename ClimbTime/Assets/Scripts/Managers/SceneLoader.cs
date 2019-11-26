using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    Player player;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Arena");
        new WaitForSeconds(3f);
        player = FindObjectOfType<Player>().GetComponent<Player>();
        player.arenaCheck = true;
        player.shoppingCheck = false;
        player.mountainCheck = false;
    }

    public void ArenaEnd()
    {
        SceneManager.LoadScene("Shopping");
        new WaitForSeconds(5f);
        player = FindObjectOfType<Player>().GetComponent<Player>();
        player.arenaCheck = false;
        player.mountainCheck = false;
        player.shoppingCheck = true;
    }

    public void Mountain()
    {
        SceneManager.LoadScene("Mountain");
        new WaitForSeconds(3f);
        player = FindObjectOfType<Player>().GetComponent<Player>();
        player.arenaCheck = false;
        player.shoppingCheck = false;
        player.mountainCheck = true;
    }
}
