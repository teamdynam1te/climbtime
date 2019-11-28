using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float destroyTime;
    public float attackPower = 1f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Obstacles")
        {
            Debug.Log("Hit");
            Destroy(gameObject);
        }

        if(other.gameObject.tag == "enemy")
        {
            Debug.Log("Enemy Hit");
            GetComponent<enemyhealth>().addDamage(1f);
        }
    }
}
