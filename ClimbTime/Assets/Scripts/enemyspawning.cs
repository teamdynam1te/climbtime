using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyspawning : MonoBehaviour
{

    public bool canSpawn;
    public int enemyCounter;
    public GameObject enemyPrefab;
    public Transform spawnPos;
    public float realTimer;
    public float timer;
    public int enemylimt;


    // Start is called before the first frame update
    void Start()
    {
        realTimer = timer;
        enemyCounter = 0;
        enemylimt = 3;
        canSpawn = true;
    }

    // Update is called once per frame
    private void Update()
    {
        realTimer -= Time.deltaTime;
       // if (enemyCounter <= enemylimt)
      // {
           
            if (canSpawn == true)
            {
                if (realTimer <= 0)
                {
                    Instantiate(enemyPrefab, spawnPos.position, Quaternion.identity);
                    realTimer = timer;
                    enemyCounter++;
                }
            }
       // }
        if (enemyCounter >= enemylimt)
        {
            canSpawn = false;
        }
       
    }
}
