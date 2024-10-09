using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TurretDatabase : ScriptableObject
{
    public TurretStats[] turrets;
    public int GetTurretsAmount()
    {
        return turrets.Length;
    }

    public TurretStats GetTurret(int index)
    {
        return turrets[index % GetTurretsAmount()];
    }
}
