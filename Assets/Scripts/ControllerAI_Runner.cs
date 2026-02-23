using UnityEngine;

public class ControllerAI_Runner : ControllerAI
{
    private float roamTurnSign = 1f;
    public override void Start()
    {
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
                    pawn.RotateTowards(target.position, pawn.turnSpeed);
                    ChangeState(AIState.Chase);
                    break;
                }
                ChangeState(AIState.ChooseRoamDirection);
                break;
            case AIState.ChooseRoamDirection:
                ChooseRoamDirection(false);
                break;
            case AIState.Unstuck:
                ChooseRoamDirection(true);
                break;
            case AIState.Roam:
                DoRoam();
                if (CanSee(target.gameObject) || CanHear(target.gameObject))
                {
                    pawn.RotateTowards(target.position, pawn.turnSpeed);
                    ChangeState(AIState.Chase);
                    break;
                }

                if (!CanMoveForward(1))
                {
                    ChangeState(AIState.Unstuck);
                }

                if (HasTimeElapsed(2f))
                {
                    if (!CanSee(target.gameObject) && !CanHear(target.gameObject))
                    {
                        ChangeState(AIState.ChooseRoamDirection);
                    }
                }
                break;
            case AIState.Chase:
                DoChase();
                pawn.Shoot();
                if (HasTimeElapsed(2f))
                {
                    if (!CanSee(target.gameObject) && !CanHear(target.gameObject))
                    {
                        ChangeState(AIState.Idle);
                    }
                }
                break;
        }
    }

    public override void DoRoam()
    {
        pawnTank.turnSpeed = 50f;
        if (roamDirection == Quaternion.identity)
        {
            float angle = Random.Range(0f, 360f);
            Vector3 dir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            roamDirection = Quaternion.LookRotation(dir);
        }

        // Rotate continuously in the chosen left/right direction while moving
        pawn.Rotate(new Vector3(roamTurnSign, 0f, 0f));
        pawn.Move(Vector3.forward);
    }

    public override void ChooseRoamDirection(bool waitToAlign)
    {
        if (roamDirection == Quaternion.identity)
        {
            if (!waitToAlign)
            {
                pawnTank.turnSpeed = 50f;
                float angle = Random.Range(0f, 360f);
                Vector3 dir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
                roamDirection = Quaternion.LookRotation(dir);
                roamTurnSign = (Random.value < 0.5f) ? -1f : 1f;
                ChangeState(AIState.Roam);
                return;
            }
            else
            {
                // Unstuck mode
                pawnTank.turnSpeed = 150f;
                float angle = (Random.value < 0.5f) ? -180f : 180f;
                Vector3 dir = Quaternion.Euler(0f, angle, 0f) * pawn.transform.forward;
                roamDirection = Quaternion.LookRotation(dir);

                roamTurnSign = (angle < 0f) ? -1f : 1f;
            }
        }

        // Otherwise rotate in place until aligned, then enter Roam.
        Vector3 desiredDir = roamDirection * Vector3.forward;
        Vector3 lookTarget = pawn.transform.position + desiredDir;
        pawn.RotateTowards(lookTarget, pawn.turnSpeed);

        float angleToDir = Vector3.Angle(pawn.transform.forward, desiredDir);
        const float alignThreshold = 5f; // leeway in degrees
        if (angleToDir <= alignThreshold)
        {
            ChangeState(AIState.Roam);
        }
    }

    public override void OnDamaged(float amount, GameObject damageSource)
    {
        target = damageSource.transform;
        ChangeState(AIState.Chase);
    }
}
