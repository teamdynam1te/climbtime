using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseResume : MonoBehaviour
{

    public GameObject PauseScreen;
    public GameObject PauseButton;
    public GameManager gm;
    public SceneLoader scene;

    bool GamePaused;


    // Start is called before the first frame update
    void Start()
    {
        GamePaused = false;
    }

    private void Awake()
    {
        gm = GetComponent<GameManager>();
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

    // Update is called once per frame
    void Update()
    {
        if (GamePaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void PauseGame()
    {
        GamePaused = true;
        PauseScreen.SetActive(true);
        PauseButton.SetActive(false);
    }

    public void ResumeGame()
    {
        GamePaused = false;
        PauseScreen.SetActive(false);
        PauseButton.SetActive(true);
    }
}