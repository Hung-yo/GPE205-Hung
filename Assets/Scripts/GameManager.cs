using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Level level;
    public GameObject playerControllerPrefab;
    public GameObject playerPawnPrefab;
    public GameObject lazyEnemyPrefab;
    public GameObject chaseEnemyPrefab;
    public GameObject runnerEnemyPrefab;
    public GameObject patrolEnemyPrefab;
    public GameObject playerCameraPrefab;
    public List<Pawn> tanks;
    public Pawn playerPawn;
    public List<Controller> players;
    public List<Camera> cameras;
    public List<PlayerSpawn> playerSpawnPoints = new List<PlayerSpawn>();
    public List<EnemySpawn> enemySpawnPoints = new List<EnemySpawn>();
    public Vector3 playerSpawnLocation;
    public Vector3 enemySpawnLocation;
    public Transform[] enemyWaypoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        tanks = new List<Pawn>();
        players = new List<Controller>();
        cameras = new List<Camera>();

    }
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        level.mapGenerator.GenerateMap();
        SpawnPlayer();
        SpawnEnemies();
    }

    public void SpawnPlayer()
    {
        Vector3 playerSpawnPosition;
        if (playerSpawnPoints == null || playerSpawnPoints.Count < 1)
        {
            Debug.Log("No spawnpoints found, defaulting to origin");
            playerSpawnPosition = Vector3.zero;
        } else
        {
            Transform playerSpawn = playerSpawnPoints[Random.Range(0, playerSpawnPoints.Count)].transform;
            playerSpawnPosition = playerSpawn.position;
        }
        
        Pawn tempTankPawn = SpawnTank(playerPawnPrefab, playerSpawnLocation);
        playerPawn = tempTankPawn;
        playerPawn.transform.position = playerSpawnPosition;
        Controller tempPlayerController = SpawnPlayerController(playerControllerPrefab);
        tempTankPawn.controller = tempPlayerController;

        Camera tempPlayerCamera = SpawnPlayerCamera(playerCameraPrefab);
        PlayerCamera playerCamera = tempPlayerCamera.GetComponent<PlayerCamera>();
        playerCamera.pawn = tempTankPawn.gameObject;
        tempTankPawn.playerCamera = playerCamera;
        tempPlayerController.Possess(tempTankPawn);

        foreach (Controller c in players)
        {
            ControllerAI ai = c as ControllerAI;
            if (ai != null)
                ai.target = tempTankPawn.transform;
        }
    }
    public void SpawnEnemies()
    {
        
    }

    public void SpawnEnemy()
    {
        if (enemySpawnPoints != null && enemySpawnPoints.Count > 0)
        {
            EnemySpawn spawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count)];
            if (spawnPoint == null)
                return;

            GameObject tempTankObject = Instantiate(patrolEnemyPrefab, spawnPoint.transform.position, Quaternion.identity);
            Pawn pawn = tempTankObject.GetComponent<Pawn>();
            if (pawn != null && !tanks.Contains(pawn))
                tanks.Add(pawn);

            // If the enemy uses patrol points, pass on patrol points from the spawner
            ControllerAI ai = tempTankObject.GetComponent<ControllerAI>();
            if (ai != null && spawnPoint.waypoints != null && spawnPoint.waypoints.Length > 0)
            {
                ai.waypoints = spawnPoint.waypoints;
            }
        }
    }

    public Pawn SpawnTank(GameObject prefab, Vector3 position)
    {
        GameObject tempTankObject = Instantiate<GameObject>(prefab, position, Quaternion.identity);
        Pawn pawn = tempTankObject.GetComponent<Pawn>();
        if (pawn != null && !tanks.Contains(pawn))
            tanks.Add(pawn);
        return pawn;
    }

    public Controller SpawnPlayerController(GameObject prefab)
    {
        GameObject tempPlayer = Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity);
        Controller ctrl = tempPlayer.GetComponent<Controller>();
        if (ctrl != null && !players.Contains(ctrl))
            players.Add(ctrl);
        return ctrl;
    }

    public Camera SpawnPlayerCamera(GameObject prefab)
    {
        GameObject tempPlayerCamera = Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.Euler(90, 0, 0));
        Camera cam = tempPlayerCamera.GetComponent<Camera>();
        if (cam != null && !cameras.Contains(cam))
            cameras.Add(cam);
        return cam;
        
        
    }
}
