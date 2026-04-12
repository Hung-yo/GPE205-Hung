using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class PickupMaxHealth : Pickup
{
    public AudioSource powerupSound;
    public static int count; // Keeps track of how many health pickups are currently spawned in
    public PowerupMaxHealth maxHealthPowerup;

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
            otherManager.Add(maxHealthPowerup);
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
