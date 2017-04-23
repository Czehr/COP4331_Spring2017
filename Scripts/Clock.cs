using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public int secondsElapsed;
    public Text clockText;
    private Puzzle puzzle;

    void Start()
    {
        secondsElapsed = 0;
        puzzle = GameObject.Find("PuzzleRoot").GetComponent<Puzzle>();
        StartCoroutine(countUpEverySecond());
    }

    void Update()
    {

    }

    IEnumerator countUpEverySecond()
    {
        while (!puzzle.pSolved())
        {
            if (puzzle.paused)
            {
                yield return new WaitForSeconds(1);
                continue;
            }

            int minutes = secondsElapsed / 60;
            int seconds = secondsElapsed % 60;
            string mString = "";
            string sString = "";

            mString = "" + minutes;

            if (seconds < 10)
                sString = "0" + seconds;
            else
                sString = "" + seconds;

            secondsElapsed += 1;

            clockText.text = mString + ":" + sString;
            yield return new WaitForSeconds(1);
        }
    }
}
