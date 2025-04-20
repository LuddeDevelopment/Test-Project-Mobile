using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ScoreManager : MonoBehaviour
{
    public static S_ScoreManager Instance;

    private float scoreTimer;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    private void Update()
    {
       // AddScoreOvertime();
    }

    private void AddScoreOvertime()
    {
        scoreTimer += Time.deltaTime;

        if (scoreTimer >= 1f)
        {
            AddScore(1);
            scoreTimer = 0f;
        }
    }

    public void AddScore(int scoreToAdd)
    {
        GameManager.Instance.currentScore += scoreToAdd;
    }
}
