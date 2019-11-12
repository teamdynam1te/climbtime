using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{

    public KeyCode fireButton;
    public int ammo;    

    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(fireButton) && ammo >= 1)
        {
            ammo--;
        }
    }
}

