using UnityEngine;

public class SpawnerTimed : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float timeBetweenSpawns;
    public bool isSpawnOnStart;
    private float countdownTimer;
    private GameObject spawnedObject;
    void Start()
    {
        if (isSpawnOnStart)
        {
            countdownTimer = 0;
        }
        else
        {
            countdownTimer = timeBetweenSpawns;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedObject == null)
        {
            countdownTimer -= Time.deltaTime;

            if (countdownTimer <= 0)
            {
                spawnedObject = Instantiate(objectToSpawn, transform.position, transform.rotation) as GameObject;
                countdownTimer = timeBetweenSpawns;
            }
        }

    }
}
