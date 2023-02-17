using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject[] ballPrefabs;

    public int ballsInScene_Red;
    public int ballsInScene_Blue;
    public int ballsInScene_Green;

    [Header("Spawn Area")]
    public Transform Up_Top;
    public Transform Up_Bottom;
    public Transform Down_Top;
    public Transform Down_Bottom;
    public Transform Left;
    public Transform Right;    

    //private GameObject[] balls;

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

        ballsInScene_Red = 0;
        ballsInScene_Blue = 0;
        ballsInScene_Green = 0;

        SpawnBalls(BallsToSpawn(), BallsToSpawn(), BallsToSpawn());
    }

    private void Start()
    {
        /*balls = GameObject.FindGameObjectsWithTag("ball");

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
        }*/
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(Left.position.x, Right.position.x);
        
        int percent = Random.Range(0, 2); // fifty-fifty chance 

        if(percent == 0)
        {
            float spawnPosZ_Up = Random.Range(Up_Bottom.position.z, Up_Top.position.z);
            return new Vector3(spawnPosX, 0.1f, spawnPosZ_Up);
        }
        else
        {
            float spawnPosZ_Down = Random.Range(Down_Bottom.position.z, Down_Top.position.z);
            return new Vector3(spawnPosX, 0.1f, spawnPosZ_Down);
        }
    }

    private void SpawnBalls(int red, int blue, int green)
    {        
        for (int i = 0; i < ballPrefabs.Length; i++)
        {
            switch (ballPrefabs[i].GetComponent<Ball>().ballColor)
            {
                case BallColor.Red:
                    for (int j = 0; j < red; j++)
                    {
                        Instantiate(ballPrefabs[i], GenerateSpawnPosition(), ballPrefabs[i].transform.rotation);
                        ballsInScene_Red++;
                    }
                    break;
                case BallColor.Blue:
                    for (int j = 0; j < blue; j++)
                    {
                        Instantiate(ballPrefabs[i], GenerateSpawnPosition(), ballPrefabs[i].transform.rotation);
                        ballsInScene_Blue++;
                    }
                    break;
                case BallColor.Green:
                    for (int j = 0; j < green; j++)
                    {
                        Instantiate(ballPrefabs[i], GenerateSpawnPosition(), ballPrefabs[i].transform.rotation);
                        ballsInScene_Green++;
                    }
                    break;
                default:
                    Debug.Log("Can not get ball color to spawn");
                    break;
            }
        }
    }

    private int BallsToSpawn()
    {
        int amount = Random.Range(1, 5);
        return amount;
    }
}
