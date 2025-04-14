using UnityEngine;

public class Armor : MonoBehaviour
{
    public Tank tank;
    public float DMGMultiplier = 1;
    public float KineticResistance = 0;
    public float ExplosiveResistance = 0;
    // Start is called before the first frame updatep
    void Start()
    {
        tank = GetComponentInParent<Tank>();
    }

    Tank GetParentTank()
    {
        Transform t = transform;
        while (t.parent != null)
        {
            if (t.parent.name == "Tank")
            {
                return t.parent.GetComponent<Tank>();
            }
            t = t.parent.transform;
        }
        Debug.Log("No parent with tag \"Tank\" found");
        return null; // Could not find a parent with given tag.
    }

    public void RegisterDamage(float damage)
    {
        tank.TakeDamage(damage * DMGMultiplier);
    }

}
