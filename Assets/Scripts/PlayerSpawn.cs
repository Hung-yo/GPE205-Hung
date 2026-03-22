using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    // Register in Awake so spawned tiles add themselves immediately when instantiated
    void Awake()
    {
        if (GameManager.instance != null)
            GameManager.instance.playerSpawnPoints.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        if (GameManager.instance != null)
            GameManager.instance.playerSpawnPoints.Remove(this);
    }
}
