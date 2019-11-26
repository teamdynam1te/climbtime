using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // git test--- you can throw this away

    public float timeLeft = 300f; //time left
    public Text levelTimer;
    Player player;
    SceneLoader scene;

    [Header("Coin Values")]
    static int coinValue = 0;

    [Header("Inventory Manager Values")]
    public int GrappleAmmoAmount;
    public int ArmourAmount;
    public int dashPotionAmount;

    public enum GameStates { init, arena, shopping, mountain, end };
    public GameStates gameState;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        scene = FindObjectOfType<SceneLoader>().GetComponent<SceneLoader>();
        player = FindObjectOfType<Player>().GetComponent<Player>();
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameStates.init:

                break;

            case GameStates.arena:

                StartCoroutine("ArenaTime");
                int seconds = Mathf.RoundToInt(timeLeft);
                levelTimer.text = string.Format("{0:D2}:{1:D2}" + " Time Remaining", (seconds / 60), (seconds % 60)); // turns timer to correct format

                break;

            case GameStates.shopping:

                break;

            case GameStates.mountain:

                break;

            case GameStates.end:

                break;
        }

    }

    IEnumerator ArenaTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            timeLeft--;

            if(timeLeft <= 0)
            {
                scene.ArenaEnd();
            }
        }
    } //countdown timer

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
        Destroy(gameObject);
    }
}
