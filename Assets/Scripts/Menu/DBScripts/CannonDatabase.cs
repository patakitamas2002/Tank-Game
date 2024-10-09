using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CannonDatabase : ScriptableObject
{
    public CannonStats[] cannons;
    public int GetCannonsAmount()
    {
        return cannons.Length;
    }

    public CannonStats GetPart(int index)
    {
        return cannons[index % GetCannonsAmount()];
    }
}
