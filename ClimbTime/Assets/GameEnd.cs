using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public SceneLoader scene;

    private void Awake()
    {
        scene = GetComponent<SceneLoader>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            scene.GameOver();
        }
    }
}
