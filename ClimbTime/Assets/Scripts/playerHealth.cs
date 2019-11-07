using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{

    [Header("health settings")]
    public float setPlayerHealth = 1;
    public float currentPlayerHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentPlayerHealth = setPlayerHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentPlayerHealth <= 0)
        {
            KillPlayer();
        }




    }

    public void KillPlayer()
    {
        this.gameObject.SetActive(false);
    }

    public void DamagePlayer(float damageToGive)
    {
        currentPlayerHealth -= damageToGive;
    }



}
