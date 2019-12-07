using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float destroyTime;
    public float attackPower = 1f;
    //public enemyhealth enemy;
    public ParticleSystem hitVFX;
    public ParticleSystem breakVFX;

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
            Instantiate(breakVFX, transform.position, Quaternion.identity);
            breakVFX.Play();
            Destroy(gameObject);
        }

        if(other.gameObject.tag == "enemy")
        {
            enemyhealth en = other.gameObject.GetComponent<enemyhealth>();
            Debug.Log("Enemy Hit");
            en.addDamage(1f);
            Instantiate(hitVFX, transform.position, Quaternion.identity);
            hitVFX.Play();
            Destroy(gameObject);
        }
    }
}
