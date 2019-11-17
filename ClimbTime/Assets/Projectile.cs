using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float destroyTime;

    private void Start()
    {
        Invoke("DestroyObject", destroyTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void DestroyObject()
    {
        if(destroyTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Obstacles")
        {
            Debug.Log("Hit");
            Destroy(gameObject);
        }
    }
}
