using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{

    public KeyCode fireButton;
    public int ammo;
    public float cooldownTime;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(fireButton) && ammo <= 1)
        {
            Shoot();
        }

    }

    public void Shoot()
    {
        ammo--;
    }
    

}

