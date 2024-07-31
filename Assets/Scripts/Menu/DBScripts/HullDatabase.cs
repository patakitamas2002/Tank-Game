using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HullDatabase : ScriptableObject
{
    public Hull[] hulls;
    public int GetHullsAmount()
    {
        return hulls.Length;
    }

    public Hull GetHull(int index)
    {
        return hulls[index % GetHullsAmount()];
    }
}
