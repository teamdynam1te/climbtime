using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chests : MonoBehaviour
{
    public int treasureValue;
    public GameManager gameManager;
    public MainSpawner spwner;
    public AudioClip pickUpSound;
    public float vol;
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
            AudioSource.PlayClipAtPoint(pickUpSound, Camera.main.transform.position, vol);
            spwner.enemyCounter--;
            Destroy(gameObject);
        }
    }
}
