using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KineticArmor : MonoBehaviour
{
    Tank tank;
    public float Thickness = 0;
    // Start is called before the first frame update
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
        return null; // Could not find a parent with given tag.
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void RegiserDamage(float damage)
    {
        tank.TakeDamage(damage);
    }
}
