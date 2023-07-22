using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text CounterText_Red;
    public Text CounterText_Blue;
    public Text CounterText_Green;

    private BallColor currentBallColor;
    private int Count_Red = 0;
    private int Count_Blue = 0;
    private int Count_Green = 0;
    private bool isRedDone;
    private bool isBlueDone;
    private bool isGreenDone;

    private void Start()
    {
        Count_Red = 0;
        Count_Blue = 0;
        Count_Green = 0;

        CounterText_Red.text = "Red: " + Count_Red + " / " + GameManager.Instance.ballsInScene_Red;
        CounterText_Blue.text = "Blue: " + Count_Blue + " / " + GameManager.Instance.ballsInScene_Blue;
        CounterText_Green.text = "Green: " + Count_Green + " / " + GameManager.Instance.ballsInScene_Green;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ball>() != null)
        {
            currentBallColor = other.GetComponent<Ball>().ballColor;

            switch (currentBallColor)
            {
                case BallColor.Red:
                    Count_Red += 1;
                    CounterText_Red.text = "Red: " + Count_Red + " / " + GameManager.Instance.ballsInScene_Red;
                    break;
                case BallColor.Blue:
                    Count_Blue += 1;
                    CounterText_Blue.text = "Blue: " + Count_Blue + " / " + GameManager.Instance.ballsInScene_Blue;
                    break;
                case BallColor.Green:
                    Count_Green += 1;
                    CounterText_Green.text = "Green: " + Count_Green + " / " + GameManager.Instance.ballsInScene_Green;
                    break;
            }

            if (isCounterDone())
            {
                GameManager.Instance.LoadNewScene();
            }
        }
    }

    private bool isCounterDone()
    {
        if (Count_Red.Equals(GameManager.Instance.ballsInScene_Red) && 
            Count_Blue.Equals(GameManager.Instance.ballsInScene_Blue) && 
            Count_Green.Equals(GameManager.Instance.ballsInScene_Green))
        {
            return true;
        }

        return false;
    }
}
