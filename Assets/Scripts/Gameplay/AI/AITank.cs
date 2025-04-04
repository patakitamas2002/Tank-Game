using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class AITank : MonoBehaviour
{

    LayerMask excludeCollisionBox;
    public int checkpointNumber { get; private set; }
    [SerializeField] public Transform[] checkpoints { get; private set; }
    public Tank tank { get; private set; }
    public Transform player { get; private set; }
    public AIStateMachine stateMachine { get; private set; }
    public NavMeshAgent agent { get; private set; }

    private GameState gameState;

    private bool isFinished { get { return checkpointNumber >= checkpoints.Length; } }

    int frametimer = 0;
    void Start()
    {
        excludeCollisionBox = ~(1 << LayerMask.NameToLayer("CollisionBox"));
        tank = GetComponent<Tank>();
        agent = GetComponent<NavMeshAgent>();
        agent.baseOffset = 3f;
        agent.updatePosition = false;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        // agent.velocity = Vector3.zero;
        // agent.isStopped = true;

        checkpointNumber = 0;


        stateMachine = new AIStateMachine(this);
        stateMachine.RegisterState(new PatrolState());
        stateMachine.RegisterState(new AttackState());
        stateMachine.ChangeState(AIStateID.Patrol);

        // StartCoroutine(Waiting());

        // Debug.Log("This is the player: " + GameObject.FindWithTag("Player"));
        player = GameObject.FindWithTag("Player").transform;
        if (player == null) Debug.Log("Player not found");
        // Debug.Log(stateMachine.currentState);
        gameState = GameObject.FindWithTag("GameState").GetComponent<GameState>();
    }
    IEnumerator Waiting()
    {
        yield return new WaitForSecondsRealtime(10);
    }
    void Update()
    {
        agent.nextPosition = transform.position;
        stateMachine.Update();
        Debug.DrawRay(transform.position + transform.forward * 6, (player.position - transform.position) * 100, Color.red);
        // Debug.Log(player.position);
        frametimer++;
        if (frametimer > 5)
        {
            frametimer = 0;
            if (stateMachine.currentState != AIStateID.Attack && CheckPlayerVisible())
                stateMachine.ChangeState(AIStateID.Attack);
            if (tank.isDead) Die();
        }

    }
    void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Triggered by: " + other.transform.position + " checkpoint: " + checkpoints[checkpointNumber].position);
        if (other.transform.position == checkpoints[checkpointNumber].position)
        {
            checkpointNumber++;
            if (stateMachine.currentState == AIStateID.Patrol)
                agent.SetDestination(checkpoints[checkpointNumber].position);
            if (isFinished) gameState.LoseGame();
        }
    }
    void Die()
    {
        enabled = false;
        agent.enabled = false;
        gameState.EnemyDefeated();

    }
    public bool CheckPlayerVisible()
    {
        if (Vector3.Distance(transform.position, player.position) > 100)
            return false;
        Vector3 relativeVector = transform.InverseTransformPoint(player.position);
        float angle = Mathf.Atan2(relativeVector.x, relativeVector.z) * Mathf.Rad2Deg;
        if (Mathf.Abs(angle) > 60) return false;
        if (!Physics.Raycast(transform.position + transform.forward * 6, player.position - transform.position, out RaycastHit hit, 100, excludeCollisionBox)) return false;
        return Vector3.Distance(hit.point, player.transform.position) > 3;
        // Debug.Log(hit.transform.name);
        // Debug.Log("Player visible");
        // return true;
    }
    public bool IsLookingAtPlayer()
    {
        Physics.Raycast(tank.barrel.transform.position, tank.barrel.transform.forward, out RaycastHit hit, 100, excludeCollisionBox);
        // Debug.Log(hit.transform.name);
        return Vector3.Distance(hit.point, player.transform.position) < 5;
    }
    public void SetCheckPoints(Transform[] points)
    {
        if (checkpoints == null) checkpoints = points;
    }
    public void Steer(Vector3 target)
    {
        Vector3 direction = transform.InverseTransformPoint(target);
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        if (Mathf.Abs(angle) > 1)
        {
            tank.Rotate(angle / Mathf.Abs(angle) * Time.deltaTime);
        }
        // Debug.Log(angle);
    }

    public void Move(float strength = 1)
    {
        if (tank.rb.velocity.magnitude < tank.hull.stats.MaxSpeed / 3)
            tank.Accelerate(strength * Time.deltaTime);
    }
    public Vector3 GetDirectionFromPosition(Vector3 target)
    {
        // return (target - transform.position).normalized;
        return transform.InverseTransformPoint(target);
    }

    private void SwitchToStrongestShell()
    {
        int strongestShell = 0;
        for (int i = 1; i < tank.barrel.shells.Length; i++)
        {

        }
    }
}
