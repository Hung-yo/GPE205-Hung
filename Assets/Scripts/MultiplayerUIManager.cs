using UnityEngine;

public class MultiplayerUIManager : MonoBehaviour
{
    public GameObject singleplayerScoreText;
    public GameObject p1ScoreText;
    public GameObject p2ScoreText;
    public GameObject singleplayerLivesText;
    public GameObject p1LivesText;
    public GameObject p2LivesText;
    public GameObject singleplayerHealthbar;
    public GameObject p1Healthbar;
    public GameObject p2Healthbar;
    public GameObject gameoverScoreText;
    public GameObject p1GameoverScoreText;
    public GameObject p2GameoverScoreText;
    
    public void GameplayToggleUI()
    {
        if (GameManager.numOfPlayers == 1)
        {
            singleplayerScoreText.SetActive(true);
            singleplayerLivesText.SetActive(true);
            singleplayerHealthbar.SetActive(true);

            p1ScoreText.SetActive(false);
            p2ScoreText.SetActive(false);
            p1LivesText.SetActive(false);
            p2LivesText.SetActive(false);
            p1Healthbar.SetActive(false);
            p2Healthbar.SetActive(false);
        }
        else if (GameManager.numOfPlayers == 2)
        {
            singleplayerScoreText.SetActive(false);
            singleplayerLivesText.SetActive(false);
            singleplayerHealthbar.SetActive(false);

            p1ScoreText.SetActive(true);
            p2ScoreText.SetActive(true);
            p1LivesText.SetActive(true);
            p2LivesText.SetActive(true);
            p1Healthbar.SetActive(true);
            p2Healthbar.SetActive(true);
        }
    }
    public void GameoverToggleUI()
    {
        if (GameManager.numOfPlayers == 1)
        {
            gameoverScoreText.SetActive(true);

            p1GameoverScoreText.SetActive(false);
            p2GameoverScoreText.SetActive(false);
        }
        else if (GameManager.numOfPlayers == 2)
        {
            gameoverScoreText.SetActive(false);

            p1GameoverScoreText.SetActive(true);
            p2GameoverScoreText.SetActive(true);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
