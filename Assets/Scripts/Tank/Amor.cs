using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Armor : MonoBehaviour
{
    Tank tank;

    void Start()
    {
        tank = getParentTank();
    }

    Tank getParentTank()
    {
        Transform t = transform;
        while (t.parent != null)
        {
            if (t.parent.tag == "Tank")
            {
                return t.parent.GetComponent<Tank>();
            }
            t = t.parent.transform;
        }
        // Could not find a parent with given tag.
        return null;
    }
    public void RegiserDamage(float damage)
    {
        tank.TakeDamage(damage);
    }
}
