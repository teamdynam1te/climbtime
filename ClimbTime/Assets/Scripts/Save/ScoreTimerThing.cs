using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTimerThing : MonoBehaviour
{
    // Start is called before the first frame update

    public float timer;
    public int timerToInt;
    public int currentTime;
    public GameManager gm;
    public bool canCount = true;
    public bool checkManager = false;
    void Start()
    {
        timer = 0;
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canCount)
        {
            timer += Time.deltaTime;
            timerToInt = Mathf.RoundToInt(timer);
            Debug.Log(timer);

            if (gm.gameState == GameManager.GameStates.end)
            {
                canCount = false;
                currentTime = timerToInt;
                Destroy(this.gameObject, 5f);
            }
        }
        

    }
}
