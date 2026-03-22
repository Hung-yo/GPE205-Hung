using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public List<Powerup> powerups;
    private Pawn pawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pawn = GetComponent<Pawn>();
        powerups = new List<Powerup>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePowerupLifespans();
        CheckForExpiredPowerups();
    }

    public void UpdatePowerupLifespans()
    {
        foreach (Powerup powerup in powerups)
        {
            powerup.lifespan -= Time.deltaTime;
        }
    }

    public void CheckForExpiredPowerups()
    {
        List<Powerup> powerupsToDestroy = new List<Powerup>();
        foreach (Powerup powerup in powerups)
        {
            if (powerup.lifespan <= 0)
            {
                powerupsToDestroy.Add(powerup);
            }
        }

        foreach (Powerup powerup in powerupsToDestroy)
        {
            Remove(powerup);
        }

    }

    public void Add(Powerup powerup)
    {
        // apply effects
        powerup.Apply(pawn);
        if (powerup.lifespan >= 0)
        {
            powerups.Add(powerup);
        }
    }

    public void Remove(Powerup powerup)
    {
        // remove effects
        powerup.Remove(pawn);
        // remove from the list
        powerups.Remove(powerup);
    }
}
