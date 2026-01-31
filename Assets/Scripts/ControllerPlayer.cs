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
        if (Input.GetKeyDown(shootKey))
        {
            pawn.Shoot();
        }
    }

    void Start()
    {
        GameManager.instance.players.Add(this);
    }
    public void OnDestroy()
    {
        GameManager.instance.players.Remove(this);
    }

    public override void Update()
    {
        base.Update();
    }
}
