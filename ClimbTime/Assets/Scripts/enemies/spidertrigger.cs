using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spidertrigger : MonoBehaviour
{
    public GameObject enemy;
    public enemymovement enemyscript;





    // Start is called before the first frame update
    void Start()
    {
        enemyscript = enemy.GetComponent<enemymovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemyscript.canmove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemyscript.canmove = false;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
