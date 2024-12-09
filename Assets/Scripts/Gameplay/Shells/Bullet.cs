using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float Caliber;
    public float Weight;
    public float Veloctiy = 700;
    bool hasCollided = false;
    public float maxTime = 20f;
    Vector3 startPosition;
    protected float remainingPen;

    void Start()
    {
        startPosition = transform.position;
        remainingPen = GetMaxPenetration();
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
        Debug.Log(collision.contacts[0].otherCollider.name);
        Armor hitArmor = collision.contacts[0].otherCollider.GetComponent<Armor>();

        if (hitArmor == null)
        {
            Debug.Log("Hit non-Armored object");
            hasCollided = true;
            return;
        };

        // Time.timeScale = 0; 
        Debug.Log("Armor thickness hit: " + hitArmor.KineticResistance);
        Debug.Log("(old)At angle: " + Vector3.Angle(transform.forward, -collision.contacts[0].normal));
        Debug.Log("At angle: " + Vector3.Angle(transform.forward, collision.contacts[0].normal));
        Debug.Log("At angle 90-: " + (Vector3.Angle(transform.forward, collision.contacts[0].normal) - 90));

        Time.timeScale = 0;
        remainingPen = CalculatePenetration(collision, distanceTravelled);
        if (remainingPen <= 0)
        {
            Debug.Log("Hit armor, no Penetration left");
            hasCollided = true;
            return;
        }

        hitArmor.RegiserDamage(CalculateDMG(collision));
        hasCollided = true;
    }
    protected abstract float GetMaxPenetration();
    protected abstract float CalculateDMG(Collision collision);
    protected abstract float CalculatePenetration(Collision collision, float distanceTravelled);

}
