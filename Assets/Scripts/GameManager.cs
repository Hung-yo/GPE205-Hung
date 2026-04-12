using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool isGameStarted;
    public static bool isGamePaused;
    public static bool isGameOver;
    public static int numOfPlayers;
    public static int p1Score;
    public static int p2Score;
    public static int p1Lives;
    public static int p2Lives;
    public Level level;
    public ScoreManager scoreManager;
    public LivesManager livesManager;
    public HealthbarManager healthbarManager;
    public GameObject playerControllerPrefab;
    public GameObject playerCompassPrefab;
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
    [Header("UI")]
    public GameObject UICamera;
    public GameObject pauseMenuUI;
    public GameObject titleScreenUI;
    public GameObject gameplayUI;
    public GameObject creditsUI;
    public GameObject startOptionsUI;
    public GameObject gameOverUI;
    public HighscoreManager highscoreManager;
    public MultiplayerUIManager multiplayerUIManager;
    public UIAudioManager uiAudioManager;
    public string[] preservedGameObjects = new string[0];

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
        MainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                UnpauseGame();

            }
            else
            {
                PauseGame();
            }
        }
    }

    public void MainMenu()
    {
        p1Score = 0;
        p2Score = 0;
        p1Lives = 1;
        p2Lives = 2;
        ClearLevelObjects();
        UICamera.SetActive(true);
        pauseMenuUI.SetActive(false);
        titleScreenUI.SetActive(true);
        gameplayUI.SetActive(false);
        creditsUI.SetActive(false);
        startOptionsUI.SetActive(false);
        gameOverUI.SetActive(false);
        isGameStarted = false;
        isGamePaused = false;
        uiAudioManager.PlayMainMenuMusic();
    }

    private void ClearLevelObjects()
    {
        var scene = SceneManager.GetActiveScene();
        var roots = scene.GetRootGameObjects();

        var preservedTagsSet = new HashSet<string>(preservedGameObjects ?? new string[0]);

        foreach (var root in roots)
        {
            if (preservedTagsSet.Count > 0 && preservedTagsSet.Contains(root.tag)) continue;

            if (root.CompareTag("DoNotDestroy")) continue;
            var children = root.GetComponentsInChildren<Transform>(true);
            bool hasDoNot = false;
            foreach (var t in children)
            {
                if (t == null || t.gameObject == null) continue;
                if (t.gameObject.CompareTag("DoNotDestroy"))
                {
                    hasDoNot = true;
                    break;
                }
            }
            if (hasDoNot) continue;

            Destroy(root);
        }
    }

    public void ShowStartOptions()
    {
        startOptionsUI.SetActive(true);
        titleScreenUI.SetActive(false);
    }
    
    public void HideStartOptions()
    {
        startOptionsUI.SetActive(false);
    }

    public void StartSingeplayerGame()
    {
        numOfPlayers = 1;
        HideStartOptions();
        level.mapGenerator.GenerateMap();
        UICamera.SetActive(false);
        SpawnPlayer("p1");
        SpawnEnemies();
        isGameStarted = true;
        titleScreenUI.SetActive(false);
        gameplayUI.SetActive(true);
        scoreManager.UpdateScore();
        livesManager.UpdateLives();
        multiplayerUIManager.GameplayToggleUI();
        uiAudioManager.PlayGameplayMusic();
    }

    public void StartMultiplayerGame()
    {
        numOfPlayers = 2;
        HideStartOptions();
        level.mapGenerator.GenerateMap();
        UICamera.SetActive(false);
        SpawnPlayer("p1");
        SpawnPlayer("p2");
        SpawnEnemies();
        isGameStarted = true;
        titleScreenUI.SetActive(false);
        gameplayUI.SetActive(true);
        scoreManager.UpdateScore();
        livesManager.UpdateLives();
        multiplayerUIManager.GameplayToggleUI();
        uiAudioManager.PlayGameplayMusic();
    }

    public void UnpauseGame()
    {
        pauseMenuUI.SetActive(false);
        isGamePaused = false;
    }
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        isGamePaused = true;
    }

    public void DisplayCredits()
    {
        creditsUI.SetActive(true);
    }

    public void HideCredits()
    {
        creditsUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        if (p1Score >= p2Score)
        {
            PlayerPrefs.SetInt("CurrentScore", p1Score);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt("CurrentScore", p2Score);
            PlayerPrefs.Save();
        }
        if (highscoreManager != null)
            highscoreManager.UpdateScore();
        isGameOver = true;
        multiplayerUIManager.GameoverToggleUI();
        gameOverUI.SetActive(true);
    }

    public static void IncreaseScore(int amount, string player)
    {
        if (player == "p1")
        {
            Debug.Log("Increased score of player 1 by " + amount);
            p1Score += amount;
            instance.scoreManager.GetComponent<ScoreManager>().UpdateScore();
        }
        else
        {
            Debug.Log("Increased score of player 2 by " + amount);
            p2Score += amount;
            instance.scoreManager.GetComponent<ScoreManager>().UpdateScore();
        }
    }

    public void SpawnPlayer(string playerID)
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
        PawnTank tank = playerPawn.GetComponent<PawnTank>();
        if (tank != null)
            tank.playerID = playerID;
        Controller tempPlayerController = SpawnPlayerController(playerControllerPrefab);
        ControllerPlayer playerController = tempPlayerController.GetComponent<ControllerPlayer>();
        if (playerController != null)
            playerController.playerID = playerID; 
        tempTankPawn.controller = tempPlayerController;

        if (healthbarManager != null)
        {
            Health health = tempTankPawn.GetComponent<Health>();
            if (health != null)
            {
                health.OnHealthChanged += (cur, max, obj) => healthbarManager.UpdateHealthbar(playerID, cur, max);
                healthbarManager.UpdateHealthbar(playerID, health.currentHealth, health.maxHealth);
            }
        }

        Camera tempPlayerCamera = SpawnPlayerCamera(playerCameraPrefab);
        PlayerCamera playerCamera = tempPlayerCamera.GetComponent<PlayerCamera>();
        playerCamera.pawn = tempTankPawn.gameObject;
        playerCamera.SetRect(playerID);
        tempTankPawn.playerCamera = playerCamera;
        tempPlayerController.Possess(tempTankPawn);

        PlayerCompass playerCompass = SpawnPlayerCompass(playerCompassPrefab);
        playerCompass.pawn = tempTankPawn.gameObject;
        playerCompass.playerID = playerID;

        foreach (Controller c in players)
        {
            ControllerAI ai = c as ControllerAI;
            if (ai != null)
                ai.target = tempTankPawn.transform;
        }
    }

    public void RespawnPlayer(GameObject pawn, string playerID)
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
        
        pawn.transform.rotation = Quaternion.identity;
        pawn.transform.position = playerSpawnPosition;
        if (playerID == "p1")
        {
            p1Lives--;
            if (p1Lives == 0)
            {
                GameOver();
            }
        }
        else if (playerID == "p2")
        {
            p2Lives--;
            if (p2Lives == 0)
            {
                GameOver();
            }
        }
        livesManager.UpdateLives();
        
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

    public PlayerCompass SpawnPlayerCompass(GameObject prefab)
    {
        GameObject tempPlayerCompass = Instantiate(prefab, Vector3.zero, Quaternion.Euler(90, 0, 0));
        PlayerCompass playerCompass = tempPlayerCompass.GetComponent<PlayerCompass>();
        return playerCompass;
    }
}
