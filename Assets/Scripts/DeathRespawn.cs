using UnityEngine;

public class DeathRespawn : Death
{
    public AudioSource deathClip;
    public GameManager gameManager;
    public PawnTank pawnTank;
    public GameObject pawn;
    public Health health;

    public void Start()
    {
        gameManager = GameManager.instance;
    }
    public override void Die()
    {
        if (deathClip != null)
        {
            AudioSource.PlayClipAtPoint(deathClip.clip, transform.position);
        }
        if (pawnTank.playerID == "p1" && GameManager.p1Lives > 0)
        {
            gameManager.RespawnPlayer(pawn, "p1");
            health.Heal(health.maxHealth);
        }
        else if (pawnTank.playerID == "p2" && GameManager.p2Lives > 0)
        {
            gameManager.RespawnPlayer(pawn, "p2");
            health.Heal(health.maxHealth);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
