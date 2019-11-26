using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public bool canSpawn;
    public int enemyCounter;
    public GameObject enemyPrefab;
    public Transform spawnPos;
    public float realTimer;
    public float timer;
    public float enemylimt = 0f;


    // Start is called before the first frame update
    void Start()
    {
        realTimer = timer;
        enemyCounter = 0;
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
                Instantiate(enemyPrefab, spawnPos.transform.position, Quaternion.identity);
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
