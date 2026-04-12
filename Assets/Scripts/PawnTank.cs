using UnityEngine;

public class PawnTank : Pawn
{
    public string playerID;
    public float shootForce;
    private Shooter shooter;
    public NoiseMaker noiseMaker;
    public override void Start()
    {
        base.Start();
        if (GameManager.instance != null && !GameManager.instance.tanks.Contains(this))
            GameManager.instance.tanks.Add(this);
        shooter = GetComponent<Shooter>();
    }
    public void OnDestroy()
    {
        GameManager.instance.tanks.Remove(this);    
    }

    public override void Move(Vector3 directionToMove)
    {
        if (noiseMaker != null)
        {
            noiseMaker.AddNoise(.1f);
        }
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
        if (noiseMaker != null)
        {
            noiseMaker.AddNoise(.1f);
        }
        shooter.Shoot();
    }

    public override void RotateTowards(Vector3 position, float turnSpeed)
    {
        mover.RotateTowards(position, turnSpeed);
    }
}
