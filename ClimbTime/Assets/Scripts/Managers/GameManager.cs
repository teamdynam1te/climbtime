using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float timeLeft = 300f; //time left
    public Text levelTimer;
    SceneLoader scene;

    private void Start()
    {
        //if statement to detect if working on correct scene
        StartCoroutine("ArenaTime"); //starts time coroutine
        scene = FindObjectOfType<SceneLoader>().GetComponent<SceneLoader>();
    }

    private void Update()
    {
        int seconds = Mathf.RoundToInt(timeLeft);
        levelTimer.text = string.Format("{0:D2}:{1:D2}" + " Time Remaining", (seconds / 60), (seconds % 60)); // turns timer to correct format
    }

    IEnumerator ArenaTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            timeLeft--;

            if(timeLeft <= 0)
            {
                scene.ArenaEnd();
            }
        }
    } //countdown timer
}
