using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour
{
    public Text scoreText;
    public ScoreManager scoreManager;

    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = scoreManager.GetScore().ToString();
    }

}
