using UnityEngine;

[System.Serializable]
public class PickupMoveSpeed : Pickup
{
    public AudioSource powerupSound;
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
            if (powerupSound != null)
            {
                AudioSource.PlayClipAtPoint(powerupSound.clip, transform.position);
            }
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
