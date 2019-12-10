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
    public float knockback = 3f;
    public float jumpKnockback = 5f;
    public enemymovement enemyMove;
    public GameObject coin;
    public float waitTime = 5;



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
            if (gm.GetScore() > 10)
            {
                while (gm.GetScore() > 0 && waitTime > 1)
                {
                    Instantiate(coin, collision.transform.position, Quaternion.identity);
                    gm.TakeScore(1);
                    waitTime--;
                }
            }
            else
            {
                while (gm.GetScore() > 0)
                {
                    Instantiate(coin, collision.transform.position, Quaternion.identity);
                    gm.TakeScore(1);
                }
            }
        }
    }

    public void DamagePlr()
    {
        
        playerMove.KnockBack(-playerMove.velocity.x * knockback, jumpKnockback);
        waitTime = 5;
    }

    

    // Update is called once per frame
    void Update()
    {
        waitTime -= Time.deltaTime;
    }
}
