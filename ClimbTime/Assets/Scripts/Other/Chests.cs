using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chests : MonoBehaviour
{
    public int treasureValue;
    public GameManager gameManager;
    public MainSpawner spwner;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        spwner = GameObject.FindGameObjectWithTag("TreasureSpawner").GetComponent<MainSpawner>();
        treasureValue = Random.Range(25, 50);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameManager.AddToScore(treasureValue);
            spwner.enemyCounter--;
            Destroy(gameObject);
        }
    }
}
