using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APHE : AP
{
    public float ExplosiveMass;
    public float FuseSensitivity;

    protected override float CalculateDMG()
    {
        Debug.Log("APHE DMG");
        float damage = 0;
        if (FuseSensitivity > hitArmor.KineticResistance)
        {
            damage = Caliber * Veloctiy / 100;
        }
        else
        {
            damage = ExplosiveMass * 20;

        }

        return damage;
    }
}
