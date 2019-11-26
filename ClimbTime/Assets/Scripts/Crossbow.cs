using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    public float offset;
    public GameObject shot;
    public Transform shotZone;
    public GameManager gm;

    private float timeBetweenShots;
    public float startTimeBetweenShots;

    public Player player;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gm.gameState == GameManager.GameStates.arena)
        {
            CanShoot();
        }
    }

    public void CanShoot()
    {
        if (gm.gameState == GameManager.GameStates.arena)
        {
            Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + offset);

            if (timeBetweenShots <= 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(shot, shotZone.position, transform.rotation);
                    timeBetweenShots = startTimeBetweenShots;
                }
            }
            else
            {
                timeBetweenShots -= Time.deltaTime;
            }
        }
    }
}
