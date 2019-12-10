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
    public float pickupTimer = 0.5f;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    private void Update()
    {
        pickupTimer -= Time.deltaTime;
    }
}

