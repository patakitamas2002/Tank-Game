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
    [SerializeField] Transform checkpointParent;
    [SerializeField] List<Transform> checkpoints;
    [SerializeField] GameState gameState;
    [SerializeField] GameObject healthBar;

    public LayerMask layerMask;

    private List<GameObject> enemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Enemy spawner start");
        foreach (Transform child in checkpointParent)
        {
            checkpoints.Add(child);
        }
        Debug.Log("CheckPoints set");
        foreach (Transform child in transform)
        {
            Debug.Log("Spawning enemy");
            gameState.AddEnemy();
            SpawnEnemy(child);
            Debug.Log("Spawned enemy: " + child.name);
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
        HealthBar hpBar = Instantiate(healthBar).GetComponent<HealthBar>();
        GameObject enemy = Tank.CreateTank(hullDatabase.hulls[hullIndex].Model, turretDatabase.turrets[turretIndex].Model, cannonDatabase.cannons[cannonIndex].Model, hpBar, position);
        AITank ai = enemy.AddComponent<AITank>();
        ai.SetCheckPoints(checkpoints.ToArray());
        // ai.tank = enemy.GetComponent<Tank>();
        enemy.AddComponent<NavMeshAgent>();
        // EnemyAI2 ai = enemy.AddComponent<EnemyAI2>();
        // ai.SetCheckPoints(checkpoints.ToArray());
        // ai.tank = enemy.GetComponent<Tank>();

        enemy.transform.SetParent(position);

        hpBar.transform.SetParent(enemy.transform);
        hpBar.transform.localPosition = Vector3.up * 5;
        ai.excludeCollisionBox = layerMask;
        enemies.Add(enemy);
    }
}
