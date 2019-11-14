using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnScript : MonoBehaviour
{

    public GameObject plr;

    private void Awake()
    {
        plr = GameObject.FindGameObjectWithTag("Player");
    }


    // Start is called before the first frame update
    void Start()
    {


        plr.transform.position = this.transform.position;
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
