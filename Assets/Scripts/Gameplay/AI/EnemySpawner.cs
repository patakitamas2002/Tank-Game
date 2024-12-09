using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] HullDatabase hullDatabase;

    [SerializeField] TurretDatabase turretDatabase;
    [SerializeField] CannonDatabase cannonDatabase;
    [SerializeField] Transform[] checkpoints;

    private List<GameObject> enemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            SpawnEnemy(child);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnEnemy(Transform position)
    {
        System.Random random = new System.Random();
        int hullIndex = random.Next(hullDatabase.hulls.Length);
        int turretIndex = random.Next(turretDatabase.turrets.Length);
        int cannonIndex = random.Next(cannonDatabase.cannons.Length);
        GameObject enemy = AITank.CreateTank(hullDatabase.hulls[hullIndex].Model, turretDatabase.turrets[turretIndex].Model, cannonDatabase.cannons[cannonIndex].Model, position);
        enemy.AddComponent<NavMeshAgent>();

        EnemyAI ai = enemy.AddComponent<EnemyAI>();
        ai.SetCheckPoints(checkpoints);

        enemy.transform.SetParent(position);
        enemies.Add(enemy);
    }
}
