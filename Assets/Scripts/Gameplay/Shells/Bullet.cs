using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float Caliber;
    public float Penetration;
    public float Weight;
    public float Veloctiy = 700;
    bool hasCollided = false;
    public float maxTime = 10f;
    Vector3 startPosition;
    protected float remainingPen;

    void Start()
    {
        startPosition = transform.position;
        remainingPen = Penetration;
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * Veloctiy, ForceMode.VelocityChange);

        Destroy(gameObject, maxTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasCollided)
        {
            return;
        }

        float distanceTravelled = Vector3.Distance(startPosition, transform.position);

        Armor hitArmor = collision.contacts[0].otherCollider.GetComponent<Armor>();
        if (hitArmor == null)
        {
            Debug.Log("Hit non-Armored object");
            hasCollided = true;
            return;
        };

        Debug.Log("Armor thickness hit: " + hitArmor.KineticResistance);
        Debug.Log("At angle: " + Vector3.Angle(transform.forward, -collision.contacts[0].normal));

        remainingPen -= CalculatePenetration(collision, distanceTravelled);
        if (remainingPen <= 0)
        {
            Debug.Log("Hit armor, no Penetration left");
            hasCollided = true;
            return;
        }

        hitArmor.RegiserDamage(CalculateDMG(collision));
        hasCollided = true;
    }
    protected abstract float CalculateDMG(Collision collision);
    protected abstract float CalculatePenetration(Collision collision, float distanceTravelled);


    public float InvSq(float value)
    {
        return 1 / (value * value);
    }
}
