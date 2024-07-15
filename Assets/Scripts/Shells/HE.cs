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
        KineticArmor weakest = null;
        Debug.Log(hitColliders.Length);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponent<KineticArmor>() == null)
                continue;

            if (weakest == null)
            {
                weakest = hitColliders[i].GetComponent<KineticArmor>();
            }
            else if (hitColliders[i].GetComponent<KineticArmor>().Thickness * InvSq(Vector3.Distance(transform.position, hitColliders[i].transform.position)) < weakest.Thickness)
            {
                weakest = hitColliders[i].GetComponent<KineticArmor>();
                Debug.Log(weakest + ": " + InvSq(Vector3.Distance(transform.position, hitColliders[i].transform.position)));
            }
        }
        Debug.Log("Weakest armor: " + weakest.Thickness);
        Debug.Log("Weakest InvSQ: " + weakest.Thickness / InvSq(Vector3.Distance(collision.transform.position, weakest.transform.position)));
        return 0;
    }
}
