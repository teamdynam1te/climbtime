﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour
{

    public float destroyTimer;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, destroyTimer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
