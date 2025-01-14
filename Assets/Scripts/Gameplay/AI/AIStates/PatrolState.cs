using UnityEngine;

public class PatrolState : AIState
{

    public void Enter()
    {
        Debug.Log("Entered Patrolling");
    }

    public void Exit()
    {

    }

    public AIStateID GetID()
    {
        return AIStateID.Patrol;
    }

    public void Update(AITank tank)
    {
        // Debug.Log("Patrolling");
        tank.Move();
        tank.Steer(tank.checkpoints[tank.checkpointNumber]);
    }

}