using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{

    public GameManager gm;
    public float HighestScore;
    public int BestTime;
    public ScoreTimerThing scoreTimerThing;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        HighestScore = PlayerPrefs.GetFloat("HighScore", 0);
        BestTime = PlayerPrefs.GetInt("BestTime", 0);
        if (gm.heightScore >= HighestScore ) // High score system
        {
            if (scoreTimerThing.currentTime < BestTime)
            {
                PlayerPrefs.SetInt("BestTime", scoreTimerThing.currentTime);
                PlayerPrefs.SetFloat("HighScore", gm.heightScore);
            }
            else
            {
                PlayerPrefs.SetFloat("HighScore", gm.heightScore);
            }
        }                                   // end high score system
        // ADD previous score system
            
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float CheckHighestScore()
    {
        return HighestScore;
    }

   /* public void SaveScore()
    {
        Debug.Log("null");
    }
    */


}
