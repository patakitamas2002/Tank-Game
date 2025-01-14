using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class AITank : MonoBehaviour
{

    public int checkpointNumber { get; private set; }
    [SerializeField] public Transform[] checkpoints { get; private set; }
    public Tank tank { get; private set; }
    public Transform player { get; private set; }

    public AIStateMachine stateMachine { get; private set; }

    int frametimer = 0;
    void Start()
    {
        checkpointNumber = 0;
        tank = GetComponent<Tank>();
        stateMachine = new AIStateMachine(this);
        stateMachine.RegisterState(new PatrolState());
        stateMachine.RegisterState(new AttackState());
        stateMachine.ChangeState(AIStateID.Patrol);
        StartCoroutine(Waiting());
        Debug.Log("This is the player: " + GameObject.FindWithTag("Player"));
        player = GameObject.FindWithTag("Player").transform;
        if (player == null) Debug.Log("Player not found");
        Debug.Log(stateMachine.currentState);

    }
    IEnumerator Waiting()
    {
        yield return new WaitForSecondsRealtime(1);
    }
    void Update()
    {
        stateMachine.Update();
        // Debug.DrawRay(transform.position + transform.forward * 6, (player.position - transform.position) * 100, Color.red);
        // Debug.Log(player.position);
        frametimer++;
        if (frametimer > 5)
        {
            frametimer = 0;
            if (stateMachine.currentState != AIStateID.Attack && CheckPlayerVisible())
                stateMachine.ChangeState(AIStateID.Attack);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.position == checkpoints[checkpointNumber].position)
        {
            checkpointNumber++;
        }
    }
    public bool CheckPlayerVisible()
    {

        // Debug.Log(hit.transform.name);
        if (Vector3.Distance(transform.position, player.position) > 100)
            return false;
        Vector3 relativeVector = transform.InverseTransformPoint(player.position);
        float angle = Mathf.Atan2(relativeVector.x, relativeVector.z) * Mathf.Rad2Deg;
        if (Mathf.Abs(angle) > 45) return false;
        if (!Physics.Raycast(transform.position + transform.forward * 6, player.position - transform.position, out RaycastHit hit, 100)) return false;
        if (Vector3.Distance(hit.point, player.transform.position) > 3) return false;

        Debug.Log("Player visible");
        return true;
    }
    public bool IsLookingAtPlayer()
    {
        Physics.Raycast(tank.barrel.transform.position, tank.barrel.transform.forward, out RaycastHit hit, 100);
        return Vector3.Distance(hit.point, player.transform.position) < 3;
    }
    public void SetCheckPoints(Transform[] points)
    {
        if (checkpoints == null) checkpoints = points;
    }
    public void Steer(Transform target)
    {
        Vector3 relativeVector = transform.InverseTransformPoint(target.position);
        float angle = Mathf.Atan2(relativeVector.x, relativeVector.z) * Mathf.Rad2Deg;

        if (Mathf.Abs(angle) > 1)
        {
            tank.Rotate(angle / Mathf.Abs(angle) * Time.deltaTime);
        }
    }
    public void Move()
    {
        if (tank.rb.velocity.magnitude < tank.hull.stats.MaxSpeed / 3)
            tank.Accelerate(1 * Time.deltaTime);
    }

    private void SwitchToStrongestShell()
    {
        int strongestShell = 0;
        for (int i = 1; i < tank.barrel.shells.Length; i++)
        {

        }
    }
}
