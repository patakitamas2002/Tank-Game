using UnityEngine;

public class AttackState : AIState
{
    int frameTimer = 0;
    float timer = 0;
    int maxTimer = 10;
    public void Enter()
    {
        Debug.Log("Entered Attack");
        timer = 0;
    }

    public void Exit()
    {

    }

    public AIStateID GetID()
    {
        return AIStateID.Attack;
    }

    public void Update(AITank tank)
    {
        tank.tank.turret.RotateTowards(tank.player);
        tank.tank.barrel.Elevate(tank.player);
        tank.Steer(tank.player);
        timer += Time.deltaTime;
        frameTimer++;
        if (frameTimer < 5 && tank.CheckPlayerVisible())
        {
            frameTimer = 0;
            if (tank.tank.barrel.reload <= 0f && tank.IsLookingAtPlayer())
            {
                tank.tank.barrel.Fire();
            }
            timer = 0;
        }
        if (timer > maxTimer)
        {
            tank.stateMachine.ChangeState(AIStateID.Patrol);
        }

    }
}