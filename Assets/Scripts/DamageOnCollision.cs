using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageOnCollision : MonoBehaviour
{
    public AudioSource deathClip;
    public float damageAmount;
    public bool destroyOnCollision = false;
    public GameObject objectFiredBy;
    public string playerID;
    private Collider _collider;
    public Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        Health otherHealth = other.GetComponent<Health>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damageAmount, objectFiredBy);
        }
        if (destroyOnCollision)
        {
            if (deathClip != null)
            {
                AudioSource.PlayClipAtPoint(deathClip.clip, transform.position);
            }
            Destroy(gameObject);
        }
    }
}
