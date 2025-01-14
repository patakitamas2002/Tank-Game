using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI2 : MonoBehaviour
{
    [SerializeField] private float speed;
    // [SerializeField] private Transform destination;
    [SerializeField] private Transform[] checkpoints;
    private int checkpointNumber = 0;
    public Tank tank;


    // void Start()
    // {
    //     Debug.Log("Skibidi skibidi");
    //     tank = GetComponent<AITank>().tank;
    //     Debug.Log("This is the tank: " + tank);
    // }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (checkpoints != null && checkpoints[checkpointNumber] != null && tank != null)
        {
            Steer();
            Move();
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.position == checkpoints[checkpointNumber].position)
        {
            checkpointNumber++;
        }
    }
    public void SetCheckPoints(Transform[] points)
    {
        if (checkpoints == null) checkpoints = points;
    }

    void Steer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(checkpoints[checkpointNumber].position);
        float angle = Mathf.Atan2(relativeVector.x, relativeVector.z) * Mathf.Rad2Deg;

        if (Mathf.Abs(angle) > 1)
        {
            tank.Rotate(angle / Mathf.Abs(angle));
        }
    }
    void Move()
    {
        if (tank.rb.velocity.magnitude < tank.hull.stats.MaxSpeed / 3)
            tank.Accelerate(1);
    }
}
