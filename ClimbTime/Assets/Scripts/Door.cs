﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    SceneLoader scene;
    
    void Start()
    {
        scene = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneLoader>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            scene.Mountain();
        }
    }
}