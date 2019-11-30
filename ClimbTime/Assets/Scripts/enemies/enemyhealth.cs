using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyhealth : MonoBehaviour
{
    public float Enemymaxhealth;

    float currenthealth;
    public GameObject coinDrop;




    // Start is called before the first frame update
    void Start()
    {
        currenthealth = Enemymaxhealth;
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
        Instantiate(coinDrop, this.transform.position, Quaternion.identity);
        Instantiate(coinDrop, this.transform.position, Quaternion.identity);
        Instantiate(coinDrop, this.transform.position, Quaternion.identity);
        Instantiate(coinDrop, this.transform.position, Quaternion.identity);
        Instantiate(coinDrop, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}
