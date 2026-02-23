using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float remainingLifespan;
    public float maxLifespan;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        remainingLifespan = maxLifespan;
    }

    // Update is called once per frame
    void Update()
    {
        remainingLifespan -= Time.deltaTime;
        if (remainingLifespan <= 0)
        {
            Destroy(gameObject);
        }
    }
}
