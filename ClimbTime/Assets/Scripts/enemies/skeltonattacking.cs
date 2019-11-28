using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeltonattacking : MonoBehaviour
{
    public GameObject plr;
    public playerHealth plrH;
    public float damage;
    public bool canDamagePlayer;

    void Start()
    {
        plr = GameObject.FindGameObjectWithTag("Player");
        plrH = plr.GetComponent<playerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("HIT");
        }
    }

    public void DamagePlr()
    {

    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
