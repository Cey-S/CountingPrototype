using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int ballsInScene_Red;
    public int ballsInScene_Blue;
    public int ballsInScene_Green;

    private GameObject[] balls;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        ballsInScene_Red = 0;
        ballsInScene_Blue = 0;
        ballsInScene_Green = 0;

        balls = GameObject.FindGameObjectsWithTag("ball");

        for (int i = 0; i < balls.Length; i++)
        {
            switch (balls[i].GetComponent<Ball>().ballColor)
            {
                case BallColor.Red:
                    ballsInScene_Red++;
                    break;
                case BallColor.Blue:
                    ballsInScene_Blue++;
                    break;
                case BallColor.Green:
                    ballsInScene_Green++;
                    break;
                default:
                    Debug.Log("Object does not have the Ball script");
                    break;
            }
        }
    }
}
