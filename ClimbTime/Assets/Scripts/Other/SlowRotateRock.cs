using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowRotateRock : MonoBehaviour
{

    public float speed;
    


    // Start is called before the first frame update
    void Start()
    {
        speed = this.GetComponentInParent<RockFallScript>().moveX;
    }

    // Update is called once per frame
    void Update()
    {

        this.transform.Rotate(0, 0, speed * speed * 5000 * Time.deltaTime);


    }
}
