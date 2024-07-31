using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APHE : AP
{
    public float ExplosiveMass;

    protected override float CalculateDMG(Collision collision)
    {
        Debug.Log("APHE DMG");
        throw new System.NotImplementedException();
    }
}
