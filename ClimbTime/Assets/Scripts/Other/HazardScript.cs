﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardScript : MonoBehaviour
{
    public Player playerMove;
    public GameObject plr;
    public playerHealth healthScript;
    public float jumpHeight = 25;

    // Start is called before the first frame update
    void Start()
    {
        plr = GameObject.FindGameObjectWithTag("Player");
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        GameObject.FindGameObjectWithTag("Player").GetComponent<playerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit");
            playerMove.Jump(jumpHeight);
            
            
            healthScript.currentPlayerHealth--;
        }
    }


}
