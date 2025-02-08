using UnityEngine;
using UnityEngine.AI;
public enum AIStateID
{
    Patrol,
    Attack
}

public interface AIState
{
    AIStateID GetID();
    void Enter(AITank tank);
    void Update(AITank tank);
    void Exit();
}