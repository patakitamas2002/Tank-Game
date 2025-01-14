using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChildFinder
{
    public static Transform FindByName(Transform parent, string name)
    {
        foreach (Transform child in parent.GetComponentInChildren<Transform>())
        {
            if (child.name == name)
            {
                return child;
            }
        }
        return null;
    }
    public static Transform FindAllByName(Transform parent, string name)
    {
        foreach (Transform child in parent.GetComponentsInChildren<Transform>())
        {
            if (child.name == name)
            {
                return child;
            }
        }
        return null;
    }

    public static Transform FindByTag(Transform parent, string tag)
    {
        foreach (Transform child in parent.GetComponentInChildren<Transform>())
        {
            Debug.Log(child.tag);
            if (child.tag == tag)
            {
                return child;
            }
        }
        return null;
    }
    public static Transform FindAllByTag(Transform parent, string tag)
    {
        foreach (Transform child in parent.GetComponentsInChildren<Transform>())
        {
            // Debug.Log(child.name);
            if (child.tag == tag)
            {
                return child;
            }
        }
        return null;
    }
}

