using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CannonDatabase : ScriptableObject
{
    public Cannon[] cannons;
    public int GetCannonsAmount()
    {
        return cannons.Length;
    }

    public Cannon GetPart(int index)
    {
        return cannons[index % GetCannonsAmount()];
    }
}
