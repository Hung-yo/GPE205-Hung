using UnityEngine;

public class LivesManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI singleplayerLivesText;
    public TMPro.TextMeshProUGUI p1LivesText;
    public TMPro.TextMeshProUGUI p2LivesText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives()
    {
        singleplayerLivesText.text = "Lives: " + GameManager.p1Lives.ToString();
        p1LivesText.text = "P1 Lives: " + GameManager.p1Lives.ToString();
        p2LivesText.text = "P2 Lives: " + GameManager.p2Lives.ToString();
    }
}
