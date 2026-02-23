using UnityEngine;

public class MoverTank : Mover
{
    private Rigidbody rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Move(Vector2 moveDirection, float moveSpeed)
    {
        Vector3 moveVector = new Vector3(moveDirection.x, 0, moveDirection.y);
        moveVector = transform.TransformDirection(moveVector);
        rb.MovePosition(rb.position + moveVector * moveSpeed * Time.deltaTime);
    }

    public override void Rotate(Vector2 rotateDirection, float turnSpeed)
    {
        float rotationAmount = rotateDirection.x;
        rotationAmount *= turnSpeed;
        rotationAmount *= Time.deltaTime;
        transform.Rotate(0, rotationAmount, 0);
    }

    public override void RotateTowards(Vector3 position, float turnSpeed)
    {
        Vector3 vectorToTarget = position - transform.position;
        // Prevent viewing vector being 0
        if (vectorToTarget.sqrMagnitude <= 1e-6f)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(vectorToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
    }

    public override void Shoot()
    {
        throw new System.NotImplementedException();
    }
}
