using UnityEngine;
using UnityEngine.AI;

public class PatrolState : AIState
{

    public void Enter(AITank tank)
    {
        Debug.Log("Entered Patrolling");
        tank.agent.SetDestination(tank.checkpoints[tank.checkpointNumber].position);
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
        // Vector3 direction = (tank.checkpoints[tank.checkpointNumber].position - tank.transform.position).normalized;
        // Debug.Log("Patrolling");
        // Vector3 direction = (tank.agent.pathEndPosition - tank.transform.position).normalized;
        // Vector3 random = tank.agent.destination;
        tank.Move();

        tank.Steer(tank.agent.steeringTarget);
    }

    void NavMeshControl(NavMeshAgent agent)
    {
        Vector3 direction = (agent.pathEndPosition - agent.transform.position).normalized;

    }
}