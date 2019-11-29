using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turn : MonoBehaviour
{
    public CoinMovement coin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacles")
        {
            //  if(coin.moveX > 0)
            // {
            //      coin.moveX = coin.randomMoveXDir[0];

            //   }
            //   else
            //   {
            //      coin.moveX = coin.randomMoveXDir[1];
            // }
            Debug.Log("turn");
        }
    }








    // Start is called before the first frame update
    void Start()
    {
        coin = gameObject.transform.parent.gameObject.GetComponent<CoinMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
