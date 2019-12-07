using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float defaultTime = 300f;
    public float heightScore = 0f;
    public float initHeight;
    public float timeLeft = 300f; //time left
    public Text levelTimer;
    public Text scoreHeightText;
    public Player player;
    public SceneLoader scene;

    [Header("Coin Values")]
    public static int coinValue = 0;

    [Header("Inventory Manager Values")]
    public int GrappleAmmoAmount;
    public int ArmourAmount;
    public int dashPotionAmount;

    public enum GameStates { init, arena, shopping, mountain, end };
    public GameStates gameState;

    public bool timerActive = false;

    public static GameManager Instance { get; private set; } = null;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (GameObject.Find(gameObject.name) && GameObject.Find(gameObject.name) != this.gameObject)
        {
            Destroy(GameObject.Find(gameObject.name));
        }
    }

    private void Start()
    {
        scene = GetComponent<SceneLoader>();
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameStates.init:
                timeLeft = defaultTime;
                ResetGame();
                //DontDestroyOnLoad(this);
                break;

            case GameStates.arena:

                levelTimer = GameObject.FindGameObjectWithTag("LevelTimer").GetComponent<Text>();
                //DontDestroyOnLoad(this);
                if (!timerActive)
                {
                    StartCoroutine("ArenaTime");
                    timerActive = true;
                }

                int seconds = Mathf.RoundToInt(timeLeft);
                levelTimer.text = string.Format("{0:D2}:{1:D2}" + " Time Left", (seconds / 60), (seconds % 60)); // turns timer to correct format

                break;

            case GameStates.shopping:
                //DontDestroyOnLoad(this);
                break;

            case GameStates.mountain:

                scoreHeightText = GameObject.FindGameObjectWithTag("ScoreHeight").GetComponent<Text>();
                levelTimer = GameObject.FindGameObjectWithTag("LevelTimer").GetComponent<Text>();
                //DontDestroyOnLoad(this);

                heightScore = player.transform.position.y - initHeight;
                scoreHeightText.text = GetHeightScore().ToString("0" + "M");

                if (!timerActive)
                {
                    StartCoroutine("MountainTime");
                    timerActive = true;
                }

                int sec = Mathf.RoundToInt(timeLeft);
                levelTimer.text = string.Format("{0:D2}:{1:D2}" + " Time Left", (sec / 60), (sec % 60));

                break;

            case GameStates.end:

                scoreHeightText = GameObject.FindGameObjectWithTag("ScoreHeight").GetComponent<Text>();
                //heightScore = player.transform.position.y - initHeight;
                scoreHeightText.text = GetHeightScore().ToString("0" + "M");
                break;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            scene.MainMenu();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            scene.QuitGame();
        }
    }

    IEnumerator ArenaTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            timeLeft--;

            if (timeLeft == 0)
            {
                scene.ArenaEnd();
                StopCoroutine("ArenaTime");
            }
        }
    } //countdown timer

    IEnumerator MountainTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            timeLeft--;

            if (timeLeft == 0)
            {
                scene.GameOver();
                StopCoroutine("MountainTime");
            }
        }
    }

    public int GetScore()
    {
        return coinValue;
    }

    public void TakeScore(int price)
    {
        coinValue -= price;
    }

    public void AddToScore(int scoreValue)
    {
        coinValue += scoreValue;
    }

    public void ResetGame()
    {
        coinValue = 0;
        heightScore = 0;
        //Destroy(gameObject);
    }

    public float GetHeightScore()
    {
        return heightScore;
    }

    public void AddToHeight(float heightValue)
    {
        heightScore += heightValue;
    }
}
