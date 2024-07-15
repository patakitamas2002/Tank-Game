using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP : Bullet
{
    //[Header("Button Settings")]
    [Tooltip("Set how much penetration the bullet has at higher angles (higher = more)")]
    public float AnglePerformance = 1f; 
    protected override float CalculateDMG(Collision collision)
    {
        //Damage based on how much remaining penetration there is, too much %pen is less damage due to less spalling
        Debug.Log("AP DMG");
        throw new System.NotImplementedException();
    }

    protected override float CalculatePenetration(Collision collision, float distanceTravelled)
    {
        //Implement DeMarr formula
        //(V / rV)^1.4283 x (D / rD)^1.0714 x (W / D^3)^0.7143 / (rW /  rD^3)^0.7143
        throw new System.NotImplementedException();
    }   
    

}
