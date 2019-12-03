using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyhealth : MonoBehaviour
{
    public float Enemymaxhealth;

    float currenthealth;
    public GameObject coinDrop;

    public int dropAmount;
    public int minDrop;
    public int maxDrop;



    // Start is called before the first frame update
    void Start()
    {
        currenthealth = Enemymaxhealth;
        dropAmount = Random.Range(minDrop, maxDrop);
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
