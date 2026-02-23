using UnityEngine;

public class ControllerAI_Chase : ControllerAI
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
                if (HasTimeElapsed(3f))
                {
                    ChangeState(AIState.ChooseRoamDirection);
                }
                break;
            case AIState.ChooseRoamDirection:
                ChooseRoamDirection();
                break;
            case AIState.Roam:
                DoRoam();
                if (CanSee(target.gameObject) || CanHear(target.gameObject))
                {
                    pawn.RotateTowards(target.position, pawn.turnSpeed);
                    ChangeState(AIState.Chase);
                    break;
                }

                if (HasTimeElapsed(2f) || !CanMoveForward(1))
                {
                    if (!CanSee(target.gameObject) && !CanHear(target.gameObject))
                    {
                        ChangeState(AIState.Idle);
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

    public override void OnDamaged(float amount, GameObject damageSource)
    {
        target = damageSource.transform;
        ChangeState(AIState.Chase);
    }
}
