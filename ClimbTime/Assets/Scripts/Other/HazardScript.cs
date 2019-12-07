using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardScript : MonoBehaviour
{
    public float jumpHeight = 25;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth plr = collision.gameObject.GetComponent<playerHealth>();
            Player move = collision.gameObject.GetComponent<Player>();
            //Debug.Log("Hit");
            move.Jump(jumpHeight);
            plr.DamagePlayer(1f);
        }
    }


}
