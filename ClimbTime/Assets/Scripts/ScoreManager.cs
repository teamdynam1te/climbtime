using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static int coinValue = 0;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberScoreManager = FindObjectsOfType<ScoreManager>().Length;
        if (numberScoreManager > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
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
        Destroy(gameObject);
    }
}
