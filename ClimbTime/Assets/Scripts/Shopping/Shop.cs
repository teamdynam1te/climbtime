using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("Required Settings")]
    public string itemTitle;
    public GameObject shopPanel;
    [Tooltip("Player Requires a score real score script. When this is added i will implement that into this.")]
    public GameObject Player;
    public GameObject shopManager;
    [Tooltip("Currently checking what input to look for.    ")]
    private bool inShop = false;
    public InventoryManager InvManager;

    [Header("Shop Settings")]
    public int itemPrice;
    [Tooltip("Mainly for Potions and Armour")]
    public int itemStock;
    public bool canBuy;
    private KeyCode buyKey;

    [Header("UI settings")]
    public Text itemTitleText;
    public Text itemPriceText;
    public Text itemStockText;

    [Header("Item Settings")]
    public bool isArmour = false;
    public bool isGrapple = false;
    public bool isDashPotion = false;
    public GameObject grapplehook;




    // Start is called before the first frame update
    void Start()
    {
        buyKey = shopManager.gameObject.GetComponent<ShopManager>().buyKey;
        InvManager = GameObject.FindGameObjectWithTag("InvManager").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        itemPriceText.text = "Price: " + itemPrice.ToString();
        if (itemStock >= 1)
        {
            itemStockText.text = "Stock: " + itemStock.ToString();
        }
        else if (itemStock < 1)
        {
            itemStockText.text = "Out of stock.";
        }
        
        itemTitleText.text = itemTitle;

        if (Player.GetComponent<PlaceHolderScoreCounter>().score >= itemPrice && itemStock >= 1)
        {
            canBuy = true;
        }
        else if (Player.GetComponent<PlaceHolderScoreCounter>().score <= itemPrice)
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
                if (isArmour)
                {
                    InvManager.ArmourAmount++;       
                }
                if (isGrapple)
                {
                    InvManager.GrappleAmmoAmount++;
                    
                }
                if (isDashPotion)
                {
                    InvManager.dashPotionAmount++;
                }
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inShop = true;
            shopPanel.GetComponent<Animator>().SetBool("HasEntered", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inShop = false;
            shopPanel.GetComponent<Animator>().SetBool("HasEntered", false);
        }
    }

}
