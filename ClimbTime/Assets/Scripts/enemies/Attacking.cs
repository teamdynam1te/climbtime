using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    public GameObject plr;
    public playerHealth plrH;
    public float damage;
    public bool canDamagePlayer;
    Animator anim;
    public bool isSkeleton = false;
    void Start()
    {
        plr = GameObject.FindGameObjectWithTag("Player");
        plrH = plr.GetComponent<playerHealth>();
        anim = this.gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            DamagePlr();
        }
    }

    public void DamagePlr()
    {
        plrH.DamagePlayer(damage);
        if (isSkeleton)
        {
         anim.SetTrigger("attack");
        }
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
