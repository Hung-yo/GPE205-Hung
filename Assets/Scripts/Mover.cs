using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    public Mover mover;
    public Controller controller;
    public abstract void Move(Vector2 moveDirection, float moveSpeed);
    public abstract void Rotate(Vector2 rotateDirection, float turnSpeed);
    public abstract void RotateTowards(Vector3 position, float turnSpeed);

    public abstract void Shoot();
}
