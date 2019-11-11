using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float timeLeft = 300f; //time left
    public Text levelTimer;

    private void Start()
    {
        StartCoroutine("LoseTime");
    }

    private void Update()
    {
        int seconds = Mathf.RoundToInt(timeLeft);
        levelTimer.text = string.Format("{0:D2}:{1:D2}" + " Time Remaining", (seconds / 60), (seconds % 60));
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }
    }
}
