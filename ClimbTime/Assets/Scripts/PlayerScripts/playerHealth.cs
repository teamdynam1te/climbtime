using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{

    [Header("health settings")]
    public float setPlayerHealth = 1;
    public float currentPlayerHealth;
    public GameManager gm;
    public SceneLoader scene;


    // Start is called before the first frame update
    void Start()
    {
        currentPlayerHealth = setPlayerHealth;
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        scene = GameObject.FindObjectOfType<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlayerHealth <= 0 && gm.ArmourAmount <= 0)
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        this.gameObject.SetActive(false);
        scene.GameOver();
    }

    public void DamagePlayer(float damageToGive)
    {
        currentPlayerHealth -= damageToGive;
    }



}
