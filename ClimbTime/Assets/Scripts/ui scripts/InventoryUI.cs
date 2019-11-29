using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    public enum UIType
    {
        potion,
        armour,
        grapple
    }
    public UIType uiType;

    public GameManager gameManager;
    public Text text;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (uiType)
        {
            case UIType.potion:
               // Debug.Log("Potion Amount: " + gameManager.dashPotionAmount);
                text.text = gameManager.dashPotionAmount.ToString();
                
            break;

            case UIType.armour:
               // Debug.Log("Armour: " + gameManager.ArmourAmount);
                text.text = gameManager.ArmourAmount.ToString();

                break;

            case UIType.grapple:
              //  Debug.Log("Grapple: " + gameManager.GrappleAmmoAmount);
                text.text = gameManager.GrappleAmmoAmount.ToString();

                break;
        }


    }
}
