using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceHolderScoreCounter : MonoBehaviour
{
    public float score;
    public Text scoreText;
    public ScoreManager scoreScript;
    public GameObject ScoreManage;


    // Start is called before the first frame update
    void Start()
    {
        ScoreManage = GameObject.FindGameObjectWithTag("ScoreManager");
        scoreScript = ScoreManage.GetComponent<ScoreManager>();

    }

    // Update is called once per frame
    void Update()
    {
        score = scoreScript.GetScore();
        scoreText.text = "Coins: " + score.ToString();  
    }
}
