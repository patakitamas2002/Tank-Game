public class AIStateMachine
{
    public AIState[] states;
    public AIStateID currentState;
    AITank tank;
    public AIStateMachine(AITank tank)
    {
        this.tank = tank;
        states = new AIState[System.Enum.GetValues(typeof(AIStateID)).Length];
    }
    public void RegisterState(AIState state)
    {
        int i = (int)state.GetID();
        states[i] = state;
    }
    public AIState GetState(AIStateID id)
    {
        return states[(int)id];
    }
    public void Update()
    {
        GetState(currentState)?.Update(tank);
    }
    public void ChangeState(AIStateID id)
    {
        GetState(currentState)?.Exit();
        currentState = id;
        Debug.Log("Changing state to: " + id);
        GetState(currentState)?.Enter(tank);
    }
}