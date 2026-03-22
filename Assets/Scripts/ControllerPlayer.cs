using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerPlayer : Controller
{
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode turnRightKey;
    public KeyCode turnLeftKey;
    public KeyCode shootKey;
    public KeyCode reloadKey;
    public KeyCode zoomNorthKey;
    public KeyCode zoomSouthKey;
    public KeyCode zoomEastKey;
    public KeyCode zoomWestKey;

    public override void MakeDecisions()
    {
        if (Input.GetKey(moveForwardKey))
        {
            pawn.Move(Vector3.forward);
        }
        if (Input.GetKey(moveBackwardKey))
        {
            pawn.Move(-Vector3.forward);
        }
        if (Input.GetKey(turnRightKey))
        {
            pawn.Rotate(-Vector3.right);
        }
        if (Input.GetKey(turnLeftKey))
        {
            pawn.Rotate(Vector3.right);
        }

        if (Input.GetKeyDown(moveForwardKey) || 
        Input.GetKeyDown(moveBackwardKey) || 
        Input.GetKeyDown(turnRightKey) || 
        Input.GetKeyDown(turnLeftKey))
        {
            pawn.playerCamera.ResetOffset();
        }
        
        if (Input.GetKeyDown(shootKey))
        {
            pawn.Shoot();
        }

        if (Input.GetKeyDown(zoomNorthKey))
        {
            pawn.playerCamera.ChangeOffsetZ(10);
        }
        if (Input.GetKeyDown(zoomSouthKey))
        {
            pawn.playerCamera.ChangeOffsetZ(-10);
        }
        if (Input.GetKeyDown(zoomEastKey))
        {
            pawn.playerCamera.ChangeOffsetX(10);
        }
        if (Input.GetKeyDown(zoomWestKey))
        {
            pawn.playerCamera.ChangeOffsetX(-10);
        }
    }

    public override void Start()
    {
        if (GameManager.instance != null && !GameManager.instance.players.Contains(this))
            GameManager.instance.players.Add(this);
    }
    public void OnDestroy()
    {
        if (GameManager.instance != null)
            GameManager.instance.players.Remove(this);
    }

    public override void Update()
    {
        base.Update();
    }
}
