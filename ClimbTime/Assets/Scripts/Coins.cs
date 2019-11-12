using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    int coinValue = 1;
    public ScoreManager scoreManager;

    public void AddCoin()
    {
        FindObjectOfType<ScoreManager>().AddToScore(coinValue);
        Debug.Log("Coin Hit");
    }
}

