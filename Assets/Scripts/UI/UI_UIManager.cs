using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_UIManager : MonoBehaviour
{
    public static UI_UIManager Instance;

    #region UI REFERENCES

    [Header("Game End Screen Settings")]
    [SerializeField] private GameObject gameEndScreen;
    [SerializeField] private TextMeshProUGUI gameEndScreenScoreText;

    [Header("Score UI Settings")]
    [SerializeField] private TextMeshProUGUI scoreText;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        gameEndScreen.SetActive(false);
    }

    private void Update()
    {
        UpdateScoreUI();
    }

    #endregion

    #region UI DISPLAY MANAGEMENT

    private void UpdateScoreUI()
    {
        scoreText.text = GameManager.Instance.currentScore.ToString("0");
    }

    public void ShowEndScreen()
    {
        gameEndScreenScoreText.text = $"SCORE : {GameManager.Instance.currentScore.ToString("0")}";
        scoreText.gameObject.SetActive(false);

        Time.timeScale = 0f;
        GameManager.Instance.EndGame();

        gameEndScreen.SetActive(true);
    }

    #endregion

    #region BUTTON EVENTS

    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion
}
