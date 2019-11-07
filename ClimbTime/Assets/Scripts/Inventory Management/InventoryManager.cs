using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    [Header("Item Select Keys")]
    public KeyCode itemKey1;
    public KeyCode itemKey2;
    public KeyCode itemKey3;

    [Header("Item Gameobjects")]
    public GameObject itemObj1;
    public GameObject itemObj2;
    public GameObject itemObj3;

    // Start is called before the first frame update
    void Start()
    {
        itemObj2.SetActive(false);
        itemObj3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(itemKey1))
        {
            itemObj1.SetActive(true);
            itemObj2.SetActive(false);
            itemObj3.SetActive(false);
        }
        else if (Input.GetKeyDown(itemKey2))
        {
            itemObj1.SetActive(false);
            itemObj2.SetActive(true);
            itemObj3.SetActive(false);
        }
        else if (Input.GetKeyDown(itemKey3))
        {
            itemObj1.SetActive(false);
            itemObj2.SetActive(false);
            itemObj3.SetActive(true);
        }
    }
}
