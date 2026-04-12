using UnityEngine;

public class DeathDestroy : Death
{
    public AudioSource deathClip;
    public override void Die()
    {
        if (deathClip != null)
        {
            AudioSource.PlayClipAtPoint(deathClip.clip, transform.position);
        }
        Destroy(gameObject);
    }
}
