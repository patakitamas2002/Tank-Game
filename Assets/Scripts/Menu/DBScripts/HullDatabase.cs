using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HullDatabase : ScriptableObject
{
    public HullStats[] hulls;
    public int GetHullsAmount()
    {
        return hulls.Length;
    }

    public HullStats GetHull(int index)
    {
        return hulls[index % GetHullsAmount()];
    }
}
