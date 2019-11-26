using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDoor : MonoBehaviour
{
    public SceneLoader scene;

    void Start()
    {
        scene = FindObjectOfType<SceneLoader>().GetComponent<SceneLoader>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            scene.MainMenu();
        }
    }
}
