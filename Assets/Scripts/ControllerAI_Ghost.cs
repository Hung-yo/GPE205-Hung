using UnityEngine;

public class ControllerAI_Ghost : ControllerAI
{
    public override void MakeDecisions()
    {
        base.MakeDecisions();
        switch(currentState)
        {
            case AIState.Idle:
                // Do nothing
                DoIdle();
                // Check for transitions
                break;
            case AIState.Roam:
                // Rotate to roam direction
                // Move forward
                DoRoam();
                break;
            case AIState.ChooseRoamDirection:
                // Choose new direction to roam
                break;
            case AIState.Attack:
                // attack actions
                break;
            case AIState.TurnAndShoot:
                // Rotate towards player
                // Shoot
                if (!CanMoveForward(5))
                {
                    ChangeState(AIState.Roam);
                }
                break;
        }
    }
}
