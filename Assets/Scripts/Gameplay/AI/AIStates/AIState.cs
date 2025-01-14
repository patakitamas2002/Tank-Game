using UnityEngine;

public enum AIStateID
{
    Patrol,
    Attack
}

public interface AIState
{
    AIStateID GetID();
    void Enter();
    void Update(AITank tank);
    void Exit();
}