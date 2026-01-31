using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerControllerPrefab;
    public GameObject playerPawnPrefab;
    public GameObject playerCameraPrefab;
    public List<Pawn> tanks;
    public List<Controller> players;
    public List<Camera> cameras;

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
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        Pawn tempTankPawn = SpawnTank(playerPawnPrefab);
        Controller tempPlayerController = SpawnPlayerController(playerControllerPrefab);

        Camera tempPlayerCamera = SpawnPlayerCamera(playerCameraPrefab);
        PlayerCamera playerCamera = tempPlayerCamera.GetComponent<PlayerCamera>();
        playerCamera.pawn = tempTankPawn.gameObject;
        tempPlayerController.Possess(tempTankPawn);
    }

    public Pawn SpawnTank(GameObject prefab)
    {
        GameObject tempTankObject = Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity);
        return tempTankObject.GetComponent<Pawn>();
    }

    public Controller SpawnPlayerController(GameObject prefab)
    {
        GameObject tempPlayer = Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity);
        return tempPlayer.GetComponent<Controller>();
    }

    public Camera SpawnPlayerCamera(GameObject prefab)
    {
        GameObject tempPlayerCamera = Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.Euler(90, 0, 0));
        return tempPlayerCamera.GetComponent<Camera>();
        
        
    }
}
