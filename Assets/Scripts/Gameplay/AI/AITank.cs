using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITank : Tank
{


    public static new GameObject CreateTank(GameObject hull, GameObject turret, GameObject barrel, Transform transform)
    {
        AITank newTank = new GameObject("AITank", typeof(AITank), typeof(Rigidbody), typeof(BoxCollider)).GetComponent<AITank>();
        newTank.transform.position = transform.position;

        newTank.hull = Instantiate(hull.GetComponent<Hull>(), newTank.transform);
        newTank.turret = Instantiate(turret.GetComponent<Turret>(), newTank.hull.transform.GetChild(0).transform);
        newTank.barrel = Instantiate(barrel.GetComponent<Barrel>(), newTank.turret.transform.GetChild(0).transform);

        return newTank.gameObject;
    }
}
