using UnityEngine;

[System.Serializable]
public class PowerupMoveSpeed : Powerup
{
    public float speedBoostAmount;
    public override void Apply(Pawn target)
    {
        // Increase the pawn's move speed
        target.IncreaseSpeed(speedBoostAmount);
    }

    public override void Remove(Pawn target)
    {
        // Take away the extra move speed
        target.IncreaseSpeed(-speedBoostAmount);
    }

}
