
using System;
using System.Numerics;

public static class MyMath
{
    public static float InvSq(float value)
    {
        return 1 / (float)Math.Pow(value + 1, 2);
    }
}