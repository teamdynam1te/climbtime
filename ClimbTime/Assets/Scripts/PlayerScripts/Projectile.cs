using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float destroyTime;
    public float attackPower = 1f;
    public AudioClip hitSFX;
    public float vol = .3f;
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
            AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, vol);
            breakVFX.Play();
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Hound")
        {
            WolfScript en = other.gameObject.GetComponent<WolfScript>();
            Debug.Log("Enemy Hit");
            en.addDamage(1f);
            Destroy(gameObject);
            Instantiate(hitVFX, transform.position, Quaternion.identity);
            hitVFX.Play();
            AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, vol);
        }

        if (other.gameObject.tag == "enemy")
        {
            enemyhealth en = other.gameObject.GetComponent<enemyhealth>();
            Debug.Log("Enemy Hit");
            en.addDamage(1f);
            Instantiate(hitVFX, transform.position, Quaternion.identity);
            hitVFX.Play();
            AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, vol);
            Destroy(gameObject);
        }
    }
}
