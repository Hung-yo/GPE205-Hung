using System.Numerics;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    protected Mover mover;
    [HideInInspector] public Controller controller;
    public float moveSpeed = 200;
    public float turnSpeed = 250;
    
    public abstract void Move(UnityEngine.Vector3 directionToMove);
    public abstract void Rotate(UnityEngine.Vector3 directionToRotate);
    public abstract void RotateTowards(UnityEngine.Vector3 position, float turnSpeed);
    public abstract void Shoot();

    public Controller GetController()
    {
        return controller;
    }

    public virtual void Start()
    {
        mover = GetComponent<Mover>();
    }
}
