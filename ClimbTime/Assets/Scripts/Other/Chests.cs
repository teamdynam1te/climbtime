﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chests : MonoBehaviour
{
    public int treasureValue;
    public GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        treasureValue = Random.Range(25, 50);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameManager.AddToScore(treasureValue);
            //audioclip
            Destroy(gameObject);
        }
    }
}
