using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardScript : MonoBehaviour
{
    public float jumpHeight = 25;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
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
            if (plr.currentPlayerHealth >= 0 && gm.ArmourAmount >= 0)
            {
                move.Jump(jumpHeight);
                gm.ArmourAmount--;
            }
            if(plr.currentPlayerHealth <= 1 && gm.ArmourAmount <= 0)
            {
                plr.DamagePlayer(1f);
            }
        }
    }


}
