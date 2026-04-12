using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerPlayer : Controller
{
    public string playerID;
    public KeyCode p1MoveForwardKey;
    public KeyCode p1MoveBackwardKey;
    public KeyCode p1TurnRightKey;
    public KeyCode p1TurnLeftKey;
    public KeyCode p1ShootKey;
    public KeyCode p1ReloadKey;
    public KeyCode p2MoveForwardKey;
    public KeyCode p2MoveBackwardKey;
    public KeyCode p2TurnRightKey;
    public KeyCode p2TurnLeftKey;
    public KeyCode p2ShootKey;
    public KeyCode p2ReloadKey;

    public override void MakeDecisions()
    {
        if (playerID == "p1")
        {
            if (Input.GetKey(p1MoveForwardKey))
            {
                pawn.Move(Vector3.forward);
            }
            if (Input.GetKey(p1MoveBackwardKey))
            {
                pawn.Move(-Vector3.forward);
            }
            if (Input.GetKey(p1TurnRightKey))
            {
                pawn.Rotate(-Vector3.right);
            }
            if (Input.GetKey(p1TurnLeftKey))
            {
                pawn.Rotate(Vector3.right);
            }
            
            if (Input.GetKeyDown(p1ShootKey))
            {
                pawn.Shoot();
            }
        }
        else if (playerID == "p2")
        {
            if (Input.GetKey(p2MoveForwardKey))
            {
                pawn.Move(Vector3.forward);
            }
            if (Input.GetKey(p2MoveBackwardKey))
            {
                pawn.Move(-Vector3.forward);
            }
            if (Input.GetKey(p2TurnRightKey))
            {
                pawn.Rotate(-Vector3.right);
            }
            if (Input.GetKey(p2TurnLeftKey))
            {
                pawn.Rotate(Vector3.right);
            }
            
            if (Input.GetKeyDown(p2ShootKey))
            {
                pawn.Shoot();
            }
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
