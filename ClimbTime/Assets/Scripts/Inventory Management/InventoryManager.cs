using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    [Header("Item Values")]
    public int GrappleAmmoAmount;
    public int ArmourAmount;
    public int dashPotionAmount;

    // Pull these values with the GUI manager\

    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
    }
}

