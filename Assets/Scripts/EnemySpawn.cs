using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform[] waypoints;
    void Awake()
    {
        if (GameManager.instance != null)
            GameManager.instance.enemySpawnPoints.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        if (GameManager.instance != null)
            GameManager.instance.enemySpawnPoints.Remove(this);
    }
}
