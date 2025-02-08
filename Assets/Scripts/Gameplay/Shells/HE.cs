using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HE : Bullet
{
    [Tooltip("The explosive mass of the shell in grams")]
    public float ExplosiveMass;


    protected override float GetMaxPenetration()
    {
        return (float)Math.Pow(ExplosiveMass, 2 / 3) / 6;
    }

    protected override float CalculateDMG()
    {
        Debug.Log("HE DMG: " + remainingPen / GetMaxPenetration());

        return ExplosiveMass * (remainingPen / GetMaxPenetration());

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
            if (!Physics.Raycast(transform.position, collision.transform.position - transform.position))
                continue;
            Debug.Log("In LoS: " + hitColliders[i].name);
            if (weakest == null)
                weakest = hitColliders[i].GetComponent<Armor>();

            else if (hitColliders[i].GetComponent<Armor>().KineticResistance * MyMath.InvSq(Vector3.Distance(transform.position, hitColliders[i].transform.position)) < weakest.KineticResistance)
            {
                weakest = hitColliders[i].GetComponent<Armor>();
                // Debug.Log(weakest + ": " + MyMath.InvSq(Vector3.Distance(transform.position, hitColliders[i].transform.position)));
            }
        }
        Debug.Log("Weakest armor: " + weakest.KineticResistance);
        Debug.Log("Weakest InvSQ: " + weakest.KineticResistance * MyMath.InvSq(Vector3.Distance(collision.transform.position, weakest.transform.position)));
        return remainingPen / MyMath.InvSq(Vector3.Distance(collision.transform.position, weakest.transform.position)) - weakest.KineticResistance;
    }


}
