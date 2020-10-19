using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    #region STATIC
    private static ScoreKeeper scoreKeeperCurrent;
    public static ScoreKeeper Get() { return scoreKeeperCurrent; }
    #endregion STATIC

    private void Awake()
    {
        scoreKeeperCurrent = this;
    }

    #region REFERENCES
    [SerializeField]
    private GameObject Player;
    private GameManager gameManager;
    #endregion REFERENCES

    #region VARIABLES

    [NonSerialized] public bool updateScore = true;
    private float currentScore=0;
    private float timePassedSinceStart=0;
    private float metersFallen=0;

    private float pointsPerMeter=0.3f;
    private float pointsPerSecond=0f;

    private float[] pointsPerCollectable= {1,25,100 };
    private float[] collectablesCollected= {0,0,0 };

    private Vector3 StartPosition=new Vector3(0,0,0);
    private Vector3 CurrentPosition=new Vector3(0,0,0);

    public bool collectedThisFrame = false;
    private bool queueCollectedThisFrame = false;
    #endregion VARIABLES

    public void UpdateCurrentScore()
    {
        if (updateScore)
        {
            float collectableScore = 0f;

            for (int i=0; i<collectablesCollected.Length; i++)
            {
                collectableScore += pointsPerCollectable[i] * collectablesCollected[i];
            }

            currentScore = Mathf.Floor(
                timePassedSinceStart * pointsPerSecond
                + collectableScore
                + metersFallen * pointsPerMeter);
        }
;
    }

    public void CollectableTaken(int collectableNumber)
    {
        collectablesCollected[collectableNumber - 1]++;
        queueCollectedThisFrame = true;
    }

    private void UpdateFallenHeight()
    {
        CurrentPosition = Player.transform.position;
        metersFallen = StartPosition.y - CurrentPosition.y;
    }

    private void Setup()
    {
        currentScore = 0;
        timePassedSinceStart = 0;
        metersFallen = 0;

        gameManager = GameManager.Get();
        //StartPosition = Player.transform.position;
    }

    private void Start()
    {
        Setup();
    }

    public float GetCurrentScore()
    {
        return currentScore;
    }

    private void Update()
    {
        if (gameManager.gameActive)
        {
            timePassedSinceStart += Time.deltaTime;
            UpdateFallenHeight();

            UpdateCurrentScore();
            //UpdateScoreUI();

        }
    }

    private void LateUpdate()
    {
        if (queueCollectedThisFrame)
        {
            collectedThisFrame = true;
            queueCollectedThisFrame = false;
        }
        else
        {
            collectedThisFrame = false;
        }
    }

}
