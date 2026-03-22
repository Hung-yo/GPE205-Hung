using UnityEngine;

[System.Serializable]
public class PowerupMaxHealth : Powerup
{
    public float maxHealthIncrease;
    public override void Apply(Pawn target)
    {
        if (target.health != null)
        {
            target.health.IncreaseMaxHealth(maxHealthIncrease);
        }
    }

    public override void Remove(Pawn target)
    {
       
    }
}
