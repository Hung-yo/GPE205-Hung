using UnityEngine;

public class PawnTank : Pawn
{
    public override void Start()
    {
        base.Start();
        GameManager.instance.tanks.Add(this);
    }
    public void OnDestroy()
    {
        GameManager.instance.tanks.Remove(this);    
    }

    public override void Move(Vector3 directionToMove)
    {
        Vector2 moveDirection = new Vector2(directionToMove.x, directionToMove.z);
        mover.Move(moveDirection, moveSpeed);
    }

    public override void Rotate(Vector3 directionToRotate)
    {
        Vector2 rotateDirection = new Vector2(directionToRotate.x, directionToRotate.z);
        mover.Rotate(rotateDirection, turnSpeed);
    }

    public override void Shoot()
    {
        Debug.Log("Shooting!");
    }
}
