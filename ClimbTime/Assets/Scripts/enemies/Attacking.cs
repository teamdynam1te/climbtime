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
    public Player playerMove;
    public float knockback = 15f;
    public enemymovement enemyMove;


    void Start()
    {
        plr = GameObject.FindGameObjectWithTag("Player");
        plrH = plr.GetComponent<playerHealth>();
        anim = this.gameObject.GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        enemyMove = GetComponentInParent<enemymovement>();
        playerMove = plr.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("HIT: " + collision.gameObject.name);
            DamagePlr();
        }
    }

    public void DamagePlr()
    {
        if (gm.GetScore() > 10)
        {
            gm.TakeScore(10);
        }
        else
        {
            gm.TakeScore(gm.GetScore());
        }
        playerMove.KnockBack(-playerMove.velocity.x * knockback, knockback);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
