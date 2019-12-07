using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spwnTime;
    public Transform[] spwnPoints;
    public int maxEnemies = 25;
    public int enemyCounter = 0;
    void Start()
    {
        spwnTime = Random.Range(0.5f, 1.5f);
        InvokeRepeating("Spawn", spwnTime, spwnTime);
    }

    void Spawn()
    {
        int spwnPointsIndex = Random.Range(0, spwnPoints.Length);

        if (enemyCounter < maxEnemies)
        {
            Instantiate(enemyPrefab, spwnPoints[spwnPointsIndex].position, Quaternion.identity);
            enemyCounter++;
        }
    }
}
