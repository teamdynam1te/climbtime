using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    int coinValue = 1;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            FindObjectOfType<ScoreManager>().AddToScore(coinValue);
            Debug.Log("Coin Hit");
        }
    }
}
