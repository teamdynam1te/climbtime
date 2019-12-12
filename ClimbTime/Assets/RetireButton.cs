using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetireButton : MonoBehaviour
{
    public GameManager gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void Retire()
    {
        gm.scene.GameOver();
    }
}
