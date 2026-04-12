using System.Collections.Generic;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI highscoreText;
    private int highscore;
    void Start()
    {
        LoadScores();
        UpdateLeaderboardDisplay();
    }

    public void UpdateScore()
    {
        int currentScore = PlayerPrefs.GetInt("CurrentScore", 0);
        if (currentScore > highscore)
        {
            highscore = currentScore;
        }
        SaveScores();
        UpdateLeaderboardDisplay();
    }

    private void LoadScores()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
    }

    private void SaveScores()
    {
        PlayerPrefs.SetInt("highscore", highscore);
        PlayerPrefs.Save();
    }

    private void UpdateLeaderboardDisplay()
    {
        highscoreText.text = "Highscore: " + highscore;
    }
}
