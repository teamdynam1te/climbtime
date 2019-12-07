using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    public int coinValue;
    public ScoreManager scoreManager;
    public AudioClip pickUpSound;
    public float vol = 1;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(pickUpSound, Camera.main.transform.position, vol);
        }
    }

    public void AddCoin()
    {
        scoreManager.AddToScore(coinValue);
    }
}

