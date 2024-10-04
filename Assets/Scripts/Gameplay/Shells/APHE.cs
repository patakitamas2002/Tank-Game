using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APHE : AP
{
    public float ExplosiveMass;
    public float FuseSensitivity;

    protected override float CalculateDMG(Collision collision)
    {
        Debug.Log("APHE DMG");
        float damage = 0;
        Armor hitArmor = collision.contacts[0].otherCollider.GetComponent<Armor>();
        if (FuseSensitivity < hitArmor.KineticResistance)
        {
            damage = Caliber * Veloctiy / 100;
        }
        else
        {
            damage = ExplosiveMass * 10;
        }

        return damage;
    }
}
