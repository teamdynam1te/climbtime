using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{

    public GameManager gm;
    public float HighestScore;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        HighestScore = PlayerPrefs.GetFloat("HighScore", 0);
        if (gm.heightScore > HighestScore)
        {
            PlayerPrefs.SetFloat("HighScore", gm.heightScore);
        }
        else
        {
            Debug.Log("Personal Best has not been beat :(");
        }
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
