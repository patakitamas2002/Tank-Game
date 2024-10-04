using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP : Bullet
{
    #region Reference Bullet
    private const float refPenentration = 139f;
    private const float refVelocity = 740f;
    private const float refCaliber = 75f;
    private const float refWeight = 6.8f;
    #endregion
    //[Header("Button Settings")]
    [Tooltip("Set how much penetration the bullet has at higher angles (higher value = worse anglepen)")]
    public float AnglePerformance = 1f;

    [Tooltip("Penetration falloff per thousand meters (1 = no falloff, must be less than 1)")]
    public float DistanceFalloff = 1f;



    protected override float GetMaxPenetration()
    {
        //Implement DeMarr formula
        // Ref Penetration *(V / rV)^1.4283 x (D / rD)^1.0714 x (W / D^3)^0.7143 / (rW /  rD^3)^0.7143
        return (float)(refPenentration / Math.Pow(Veloctiy / refVelocity, 1.4283) *
            Math.Pow(Caliber / refCaliber, 1.0714) * Math.Pow(refWeight / refCaliber, 0.7143) / Math.Pow(refPenentration / refCaliber, 0.7143));
    }

    protected override float CalculateDMG(Collision collision)
    {
        //Damage based on how much remaining penetration there is, too much %pen is less damage due to less spalling
        float maxDamage = Caliber * Veloctiy / 100;
        // float penPercent =

        Debug.Log("AP DMG");
        return maxDamage;
        throw new System.NotImplementedException();
    }

    protected override float CalculatePenetration(Collision collision, float distanceTravelled)
    {
        Armor hitArmor = collision.contacts[0].otherCollider.GetComponent<Armor>();
        double rad = Vector3.Angle(transform.forward, -collision.contacts[0].normal) * Mathf.Deg2Rad;


        float effectivePen = remainingPen * (float)Math.Pow(DistanceFalloff, distanceTravelled / 1000); //Distance falloff
        effectivePen = effectivePen * (float)Math.Pow(Math.Cos(rad), AnglePerformance); //Angle falloff

        Debug.Log("EffectivePen: " + effectivePen);
        return effectivePen - hitArmor.KineticResistance;
    }
}
