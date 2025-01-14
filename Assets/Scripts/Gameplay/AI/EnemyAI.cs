using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform destination;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform[] checkpoints;
    private int checkpointNumber = 0;
    private Tank tank;

    // Start is called before the first frame update

    void Start()
    {
        SetFields();
        agent.SetDestination(checkpoints[checkpointNumber].position);
    }
    // Update is called once per frame
    void Update()
    {
        if (checkpoints != null && checkpoints[0] != null)
        {

            //agent.transform.LookAt(checkpoints[checkpointNumber].position);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.position == checkpoints[checkpointNumber].position)
        {
            checkpointNumber++;
            agent.SetDestination(checkpoints[checkpointNumber].position);
        }
    }

    void SetFields()
    {
        agent = GetComponent<NavMeshAgent>();
        tank = GetComponent<AITank>().tank;

        agent.angularSpeed = tank.rotationSpeed;
        agent.acceleration = tank.accelSpeed / 20;
        agent.speed = tank.hull.stats.MaxSpeed / 2;
        agent.stoppingDistance = 10;
        agent.radius = 1f;
        agent.height = 2.5f;
        agent.baseOffset = 1.1f;
        // agent.agentTypeID = 1;
        Debug.Log("Agent id:" + agent.agentTypeID);
    }

    public void SetCheckPoints(Transform[] points)
    {
        if (checkpoints == null) checkpoints = points;
    }
}
