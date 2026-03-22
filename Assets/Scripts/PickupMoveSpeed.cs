using UnityEngine;

[System.Serializable]
public class PickupMoveSpeed : Pickup
{
    public static int count;
    public PowerupMoveSpeed moveSpeedPowerup;
   public override void Start()
    {
        count++;
        base.Start();
    }
    public override void OnTriggerEnter(Collider other)
    {
        PowerupManager otherManager = other.GetComponent<PowerupManager>();

        if (otherManager != null)
        {
            otherManager.Add(moveSpeedPowerup);
            Destroy(gameObject);
        }

        base.OnTriggerEnter(other);
    }

    public override void OnDestroy()
    {
        count--;
        base.OnDestroy();
    }
}
