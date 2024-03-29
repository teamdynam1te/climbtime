﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyhealth : MonoBehaviour
{
    public float Enemymaxhealth;

    float currenthealth;
    public GameObject coinDrop;
    public MainSpawner spwn;

    public int dropAmount;
    public int minDrop;
    public int maxDrop;
    public bool isHound;
    public GameObject houndParticle;
    Collider2D collider;

    Animator anim;

    enemymovement move;

    // Start is called before the first frame update
    void Start()
    {
        currenthealth = Enemymaxhealth;
        spwn = GameObject.FindGameObjectWithTag("SkelSpawner").GetComponent<MainSpawner>();
        collider = GetComponent<Collider2D>();
        dropAmount = Random.Range(minDrop, maxDrop);
        anim = this.gameObject.GetComponent<Animator>();
        move = this.gameObject.GetComponent<enemymovement>();
        
    }

    public void addDamage(float damage)
    {
        currenthealth -= damage;
        if (currenthealth <= 0)
        {
            spwn.enemyCounter--;
            makeDead();
        }
    }

    void makeDead()
    {
       while (dropAmount > 0)
        {
            DropCoins(); 
        }
        anim.SetTrigger("Die");
        move.moveSpeed  = 0f;
        if (isHound)
        {
            Instantiate(houndParticle, this.transform.position, Quaternion.identity);
        }
        Destroy(gameObject, 1f);
    }

    public void DropCoins()
    {
        Instantiate(coinDrop, this.transform.position, Quaternion.identity);
        dropAmount--;
    }
}
