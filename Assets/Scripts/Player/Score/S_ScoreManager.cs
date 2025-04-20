using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ScoreManager : MonoBehaviour
{
    public static S_ScoreManager Instance;

    #region PRIVATE VARIABLES

    private float scoreTimer;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        // Uncomment to enable passive score increase
        // AddScoreOverTime();
    }

    #endregion

    #region SCORE MANAGEMENT

    private void AddScoreOverTime()
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

    #endregion
}
