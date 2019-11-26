using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour
{
    public Text scoreText;
    public GameObject scoremanagerobj;
    public ScoreManager scoreManager;

    void Start()
    {
        //scoreText = GetComponent<Text>();
        scoremanagerobj = GameObject.FindGameObjectWithTag("ScoreManager");
        scoreManager = scoremanagerobj.GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = scoreManager.GetScore().ToString();
    }

}
