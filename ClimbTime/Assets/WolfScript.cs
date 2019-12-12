using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfScript : MonoBehaviour
{
    public float Enemymaxhealth;

    float currenthealth;
    public GameObject coinDrop;
    public MainSpawner spwner;

    public int dropAmount;
    public int minDrop;
    public int maxDrop;

    Collider2D collider;

    Animator anim;

    enemymovement move;

    // Start is called before the first frame update
    void Start()
    {
        currenthealth = Enemymaxhealth;
        spwner = GameObject.FindGameObjectWithTag("HnDSpawn").GetComponent<MainSpawner>();
        collider = GetComponent<Collider2D>();
        dropAmount = Random.Range(minDrop, maxDrop);
    }

    public void addDamage(float damage)
    {
        currenthealth -= damage;
        if (currenthealth <= 0)
        {
            spwner.enemyCounter--;
            makeDead();
        }
    }

    void makeDead()
    {
        while (dropAmount > 0)
        {
            DropCoins();
        }
        Destroy(gameObject);
    }

    public void DropCoins()
    {
        Instantiate(coinDrop, this.transform.position, Quaternion.identity);
        dropAmount--;
    }
}

