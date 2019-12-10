using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    public GameObject plr;
    public playerHealth plrH;
    public float damage;
    public bool canDamagePlayer;
    public GameManager gm;
    Animator anim;
    public bool isSkeleton = false;
    void Start()
    {
        plr = GameObject.FindGameObjectWithTag("Player");
        plrH = plr.GetComponent<playerHealth>();
        anim = this.gameObject.GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
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
        gm.TakeScore(10);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
