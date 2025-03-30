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
        if (remainingPen <= 0)
            return 0;
        return ExplosiveMass * (remainingPen / GetMaxPenetration());

    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HE Collision");
        if (hasCollided)
        {
            return;
        }
        hasCollided = true;
        float distanceTravelled = Vector3.Distance(startPosition, transform.position);
        remainingPen = CalculatePenetration(collision, distanceTravelled);
        if (hitArmor != null)
            hitArmor.RegisterDamage(CalculateDMG());

        Destroy(this, 0.2f);
    }

    // protected override float CalculatePenetration(Collision collision, float distanceTravelled)
    // {
    //     Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f);
    //     Armor weakest = null;
    //     Debug.Log(hitColliders.Length);
    //     for (int i = 0; i < hitColliders.Length; i++)
    //     {
    //         if (hitColliders[i].GetComponent<Armor>() == null)
    //             continue;
    //         if (!Physics.Raycast(transform.position, collision.transform.position - transform.position))
    //             continue;
    //         Debug.Log("In LoS: " + hitColliders[i].name);
    //         if (weakest == null)
    //             weakest = hitColliders[i].GetComponent<Armor>();

    //         else if (hitColliders[i].GetComponent<Armor>().KineticResistance * MyMath.InvSq(Vector3.Distance(transform.position, hitColliders[i].transform.position)) < weakest.KineticResistance)
    //         {
    //             weakest = hitColliders[i].GetComponent<Armor>();
    //             // Debug.Log(weakest + ": " + MyMath.InvSq(Vector3.Distance(transform.position, hitColliders[i].transform.position)));
    //         }
    //     }
    //     Debug.Log("Weakest armor: " + weakest.KineticResistance);
    //     Debug.Log("Weakest InvSQ: " + weakest.KineticResistance * MyMath.InvSq(Vector3.Distance(collision.transform.position, weakest.transform.position)));
    //     return remainingPen / MyMath.InvSq(Vector3.Distance(collision.transform.position, weakest.transform.position)) - weakest.KineticResistance;
    // }


    protected override float CalculatePenetration(Collision collision, float distanceTravelled)
    {
        float armor = GetWeakestArmorThickness(collision);
        Debug.Log("Armor thickness hit: " + armor);
        return remainingPen - armor;
    }
    float GetWeakestArmorThickness(Collision collision)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3f);
        float weakest = float.MaxValue;
        for (int i = 0; i < hitColliders.Length; i++)
        {

            Debug.DrawRay(transform.position, hitColliders[i].transform.position - transform.position, Color.blue, 5f);
            if (Physics.Raycast(
                transform.position, hitColliders[i].transform.position - transform.position,
                out RaycastHit hit, 10f, layerMask: ~(1 << LayerMask.NameToLayer("CollisionBox"))))
            {
                Debug.Log(hit.collider.name + " - " + hitColliders[i].transform.name);
                if (hit.collider.transform != hitColliders[i].transform)
                    continue;
            }


            Armor armor = hitColliders[i].GetComponent<Armor>();
            if (armor == null) continue;
            float resistance = GetResistance(hitColliders[i], armor);
            Debug.Log(armor.name + ": " + resistance);
            if (weakest == 0)
                weakest = resistance;

            else if (resistance < weakest)
            {
                hitArmor = armor;
                weakest = resistance;
            }
        }
        return weakest;
    }

    float GetResistance(Collider collider, Armor armor)
    {
        float distance = Vector3.Distance(transform.position, collider.ClosestPoint(transform.position));
        return armor.KineticResistance / MyMath.InvSq(distance);
    }
}
