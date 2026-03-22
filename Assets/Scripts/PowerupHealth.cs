using UnityEngine;

[System.Serializable]
public class PowerupHealth : Powerup
{
    public float amountToHeal;
    public override void Apply(Pawn target)
    {
        if (target.health != null)
        {
            target.health.Heal(amountToHeal);
        }
    }

    public override void Remove(Pawn target)
    {
        
    }
}
