void OnTriggerEnter(Collider other)
{
    if (other.transform.position == checkpoints[checkpointNumber].position)
    {
        checkpointNumber++;
        if (stateMachine.currentState == AIStateID.Patrol)
            agent.SetDestination(checkpoints[checkpointNumber].position);
        if (isFinished) gameState.LoseGame();
    }
}