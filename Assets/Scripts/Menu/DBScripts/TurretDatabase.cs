using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TurretDatabase : ScriptableObject
{
    public Turret[] turrets;
    public int GetTurretsAmount()
    {
        return turrets.Length;
    }

    public Turret GetTurret(int index)
    {
        return turrets[index % GetTurretsAmount()];
    }
}
