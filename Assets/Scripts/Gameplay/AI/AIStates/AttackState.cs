using UnityEngine;

public class AttackState : AIState
{
    int frameTimer = 0;
    float timer = 0;
    int maxTimer = 10;
    public void Enter(AITank tank)
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

    // public void Update(AITank tank)
    // {

    //     timer += Time.deltaTime;
    //     frameTimer++;
    //     if (frameTimer > 5 && tank.CheckPlayerVisible())
    //     {
    //         LookAtPlayer();
    //         timer = 0;
    //         frameTimer = 0;
    //         if (tank.tank.barrel.reload <= 0f && tank.IsLookingAtPlayer())
    //         {
    //             Debug.Log("AI Fired");
    //             tank.tank.barrel.Fire();
    //         }

    //     }
    //     else
    //     {
    //         FollowPlayer(tank);
    //     }
    //     if (timer > maxTimer)
    //     {
    //         tank.stateMachine.ChangeState(AIStateID.Patrol);
    //     }
    // }
    public void Update(AITank tank)
    {
        timer += Time.deltaTime;
        frameTimer++;
        if (frameTimer > 5 && tank.CheckPlayerVisible())
        {
            timer = 0;
            LookAtPlayer(tank);

            if (tank.tank.barrel.reload <= 0f && tank.IsLookingAtPlayer())
            {
                Debug.Log("AI Fired");
                tank.tank.barrel.Fire();
            }
        }
        else
        {
            FollowPlayer(tank);
        }
        if (frameTimer >= 5) frameTimer = 0;
        if (timer > maxTimer) tank.stateMachine.ChangeState(AIStateID.Patrol);
    }
    void FollowPlayer(AITank tank)
    {
        tank.Move(0.5f);
        tank.Steer(tank.agent.steeringTarget);

        if (frameTimer != 5) return;
        tank.agent.SetDestination(tank.player.position);

    }
    void LookAtPlayer(AITank tank)
    {
        tank.tank.turret.RotateTowards(tank.player);
        tank.tank.barrel.Elevate(tank.player);
        tank.Steer(tank.GetDirectionFromPosition(tank.player.position));
    }

}