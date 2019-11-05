using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Required Settings")]
    public Animator anim;
    [Tooltip("Player Requires a score real score script. When this is added i will implement that into this.")]
    public GameObject Player;
    public GameObject shopManager;
    [Tooltip("Currently checking what input to look for.    ")]
    private bool inShop = false;
    

    [Header("Shop Settings")]
    public int itemPrice;
    [Tooltip("Mainly for Potions and Armour")]
    public int itemStock;
    public bool canBuy;
    private KeyCode buyKey;



    // Start is called before the first frame update
    void Start()
    {
        buyKey = shopManager.gameObject.GetComponent<ShopManager>().buyKey;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<PlaceHolderScoreCounter>().score >= itemPrice && itemStock >= 1)
        {
            canBuy = true;
        }
        else if (Player.GetComponent<PlaceHolderScoreCounter>().score >= itemPrice)
        {
            canBuy = false;
        }
        else if (itemStock < 1)
        {
            canBuy = false;
        }



        if (inShop == true && canBuy == true)
        {
            if (Input.GetKeyDown(buyKey))
            {
                Debug.Log("Buy");
                Player.GetComponent<PlaceHolderScoreCounter>().score -= itemPrice;
                itemStock--;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inShop = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inShop = false;
        }
    }

}
