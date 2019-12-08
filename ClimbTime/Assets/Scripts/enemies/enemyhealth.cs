using System.Collections;
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

    // Update is called once per frame
    void Update()
    {
       
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

        Destroy(gameObject, 1f);
    }

    public void DropCoins()
    {
        Instantiate(coinDrop, this.transform.position, Quaternion.identity);
        dropAmount--;
    }
}
