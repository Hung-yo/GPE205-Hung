using Unity.VisualScripting;
using UnityEngine;

public class ControllerAI_Lazy : ControllerAI
{
    public bool hasFleed;
    public float fleeDuration = 5f;

    public override void Start()
    {
        health = GetComponent<Health>();
        hasFleed = false;
        base.Start();
    }

    public override void MakeDecisions()
    {
        base.MakeDecisions();
        switch(currentState)
        {
            case AIState.Idle:
                DoIdle();
                if (CanSee(target.gameObject) || CanHear(target.gameObject))
                {
                    ChangeState(AIState.TurnAndShoot);
                    break;
                }
                if (HasTimeElapsed(5f))
                {
                    ChangeState(AIState.ChooseRoamDirection);
                }
                break;
            case AIState.ChooseRoamDirection:
                ChooseRoamDirection(true);
                break;
            case AIState.TurnAndShoot:
                //pawn.Move(Vector3.zero);
                pawn.RotateTowards(target.position, pawn.turnSpeed);
                pawn.Shoot();
                if (HasTimeElapsed(2f))
                {
                    if (!CanSee(target.gameObject) && !CanHear(target.gameObject))
                    {
                        ChangeState(AIState.Idle);
                    }
                }

                if (health.currentHealth < 3 && !hasFleed)
                {
                    ChangeState(AIState.Flee);
                }
                break;
            case AIState.Flee:
                DoFlee();
                break;
        }
    }

    public override void ChooseRoamDirection(bool waitUntilAligned)
    {
        // Pick a new random direction only once when entering this state
        if (roamDirection == Quaternion.identity)
        {
            float angle = Random.Range(0f, 360f);
            Vector3 dir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            roamDirection = Quaternion.LookRotation(dir);
        }

        // Rotate each frame towards the chosen direction until aligned
        Vector3 desiredDir = roamDirection * Vector3.forward;
        Vector3 lookTarget = pawn.transform.position + desiredDir;
        pawn.RotateTowards(lookTarget, pawn.turnSpeed);

        float angleToDir = Vector3.Angle(pawn.transform.forward, desiredDir);
        const float alignThreshold = 5f; // leeway in degrees
        if (angleToDir <= alignThreshold)
        {
            ChangeState(AIState.Idle);
        }
    }
    public override void DoFlee()
    {
        if (!hasFleed)
        {
            hasFleed = true;
        }

        Vector3 vectorToTarget = pawn.transform.position - target.position;
        float distanceToPlayer = vectorToTarget.magnitude;
        vectorToTarget.Normalize();

        float percentOfFleeDistance = distanceToPlayer / fleeDistance;
        percentOfFleeDistance = Mathf.Clamp01(percentOfFleeDistance);
        float flippedPercentOfFleeDistance = 1 - percentOfFleeDistance;
        float newFleeDistance = flippedPercentOfFleeDistance * fleeDistance;

        Vector3 targetPosition = pawn.transform.position + (vectorToTarget * newFleeDistance);
        Seek(targetPosition);

        bool collided = !CanMoveForward(1f);
        bool timeElapsed = HasTimeElapsed(fleeDuration);

        if (collided)
        {
            pawn.Move(Vector3.zero);
            Rigidbody rb = pawn.GetComponent<Rigidbody>();
            if (rb != null)
                rb.linearVelocity = Vector3.zero;
            if (CanSee(target.gameObject) || CanHear(target.gameObject))
                ChangeState(AIState.TurnAndShoot);
            else
                ChangeState(AIState.Idle);
            return;
        }

        if (timeElapsed)
        {
            if (CanSee(target.gameObject) || CanHear(target.gameObject))
                ChangeState(AIState.TurnAndShoot);
            else
                ChangeState(AIState.Idle);
            return;
        }
    }
}
