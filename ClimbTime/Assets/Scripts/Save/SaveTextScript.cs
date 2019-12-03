using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveTextScript : MonoBehaviour
{
    public enum TextType { HighestScore, PreviousScore }
    public TextType textType;
    public Text txt;
    public SaveGame saveScript;
    public int HighScoreInt;

    // Start is called before the first frame update
    void Start()
    {
        saveScript = GameObject.FindGameObjectWithTag("HighScoreThing").GetComponent<SaveGame>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (textType)
        {
            case TextType.HighestScore:
                if (saveScript.CheckHighestScore() <= 0)
                {
                    txt.text = "Play the game and you will see your highest score here!";
                }
                else if (saveScript.CheckHighestScore() <= 279)
                {
                    HighScoreInt = Mathf.RoundToInt(saveScript.CheckHighestScore());
                    txt.text = "Current Highest score: " + HighScoreInt.ToString() + "m";
                }
                else if (saveScript.CheckHighestScore() >= 280)
                {
                    txt.text = "Congrats! You have climbed up the 280m tall mountain!";
                }
                break;
        }

    }

}
