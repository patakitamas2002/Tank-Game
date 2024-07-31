using UnityEngine;

public class Armor : MonoBehaviour
{
    Tank tank;
    public float KineticResistance = 0;
    public float ExplosiveResistance = 0;
    // Start is called before the first frame updatep
    void Start()
    {
        tank = GetParentTank();
    }

    Tank GetParentTank()
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

    public void RegiserDamage(float damage)
    {
        tank.TakeDamage(damage);
    }
}
