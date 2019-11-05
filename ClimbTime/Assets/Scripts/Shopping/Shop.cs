using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Required Settings")]
    public Animator anim;
    private bool inShop = false;

    [Header("Shop Settings")]
    public int itemPrice;
    [Tooltip("Mainly for Potions and Armour")]
    public int itemStock;
    public Input buyKey;

    [Header("Shop Input Settings")]
    public KeyCode inputKey;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (inShop == true)
        {
            if (Input.GetKeyDown(inputKey))
            {
                Debug.Log("Buy item");
                
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
