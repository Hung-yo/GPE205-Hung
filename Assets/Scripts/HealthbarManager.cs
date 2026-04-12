using UnityEngine;
using UnityEngine.UI;

public class HealthbarManager : MonoBehaviour
{
    public Image singleplayerHealthbar;
    public Image p1Healthbar;
    public Image p2Healthbar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthbar(string playerID, float currentHealth, float maxHealth)
    {
        if (maxHealth <= 0f)
            return;

        float fill = Mathf.Clamp01(currentHealth / maxHealth);
        if (playerID == "p1")
        {
            p1Healthbar.fillAmount = fill;
            singleplayerHealthbar.fillAmount = fill;
        }
        else if (playerID == "p2")
            p2Healthbar.fillAmount = fill;
    }
}
