using UnityEngine;

[RequireComponent (typeof(Collider))]
public class Pickup : MonoBehaviour
{
    public Powerup powerup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        Collider pickupCollider = GetComponent<Collider>();
        pickupCollider.isTrigger = true;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        PowerupManager otherManager = other.GetComponent<PowerupManager>();

        if (otherManager != null)
        {
            
        }

        Destroy(gameObject);
    }

    public virtual void OnDestroy()
    {
        
    }
}
