using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HE : Bullet
{
    public float ExplosiveMass;


    protected override float CalculateDMG(Collision collision)
    {
        Debug.Log("HE DMG");
        throw new System.NotImplementedException();

    }

    protected override float CalculatePenetration(Collision collision, float distanceTravelled)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f);
        Armor weakest = null;
        Debug.Log(hitColliders.Length);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponent<Armor>() == null)
                continue;

            if (weakest == null)
            {
                weakest = hitColliders[i].GetComponent<Armor>();
            }
            else if (hitColliders[i].GetComponent<Armor>().KineticResistance * InvSq(Vector3.Distance(transform.position, hitColliders[i].transform.position)) < weakest.KineticResistance)
            {
                weakest = hitColliders[i].GetComponent<Armor>();
                Debug.Log(weakest + ": " + InvSq(Vector3.Distance(transform.position, hitColliders[i].transform.position)));
            }
        }
        Debug.Log("Weakest armor: " + weakest.KineticResistance);
        Debug.Log("Weakest InvSQ: " + weakest.KineticResistance / InvSq(Vector3.Distance(collision.transform.position, weakest.transform.position)));
        return 0;
    }
}
