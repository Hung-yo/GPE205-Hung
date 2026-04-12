using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI singleplayerScoreText;
    public TMPro.TextMeshProUGUI p1ScoreText;
    public TMPro.TextMeshProUGUI p2ScoreText;
    public TMPro.TextMeshProUGUI gameoverScoreText;
    public TMPro.TextMeshProUGUI p1GameoverScoreText;
    public TMPro.TextMeshProUGUI p2GameoverScoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore()
    {
        // Gameplay UI
        singleplayerScoreText.text = "Score: " + GameManager.p1Score.ToString();
        p1ScoreText.text = "P1 Score: " + GameManager.p1Score.ToString();
        p2ScoreText.text = "P2 Score: " + GameManager.p2Score.ToString();
        // Gameover UI
        gameoverScoreText.text = "Your Score: " + GameManager.p1Score.ToString();
        p1GameoverScoreText.text = "Player 1 Score: " + GameManager.p1Score.ToString();
        p2GameoverScoreText.text = "Player 2 Score: " + GameManager.p2Score.ToString();
    }
}
