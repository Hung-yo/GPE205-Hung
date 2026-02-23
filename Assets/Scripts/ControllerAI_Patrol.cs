using UnityEngine;

public class ControllerAI_Patrol : ControllerAI
{
    public override void MakeDecisions()
    {
        base.MakeDecisions();
        switch(currentState)
        {
            case AIState.Idle:
                DoIdle();
                if (HasTimeElapsed(3f))
                {
                    ChangeState(AIState.Patrol);
                }
                break;
            case AIState.Patrol:
                DoPatrol();
                if (!CanMoveForward(1))
                {
                    ChangeState(AIState.Unstuck);
                }
                break;
            case AIState.Unstuck:
                ChooseRoamDirection();
                ChangeState(AIState.Patrol);
                break;
            case AIState.TurnAndShoot:
                pawn.RotateTowards(target.position, pawn.turnSpeed);
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
}
