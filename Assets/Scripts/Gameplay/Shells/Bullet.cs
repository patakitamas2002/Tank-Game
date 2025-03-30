using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [Tooltip("The caliber of the shell in milimeters")]
    public float Caliber;
    [Tooltip("The mass of the shell in kilograms")]
    public float Weight;
    [Tooltip("The speed of the shell in meters per second")]
    public float Veloctiy = 700;
    protected bool hasCollided = false;
    public float maxTime = 20f;
    protected Vector3 startPosition;
    protected float remainingPen;
    protected Armor hitArmor;


    void Start()
    {
        startPosition = transform.position;
        remainingPen = GetMaxPenetration();
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * Veloctiy, ForceMode.VelocityChange);
        transform.localScale = Vector3.one * Caliber / 100;

        Destroy(gameObject, maxTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (hasCollided)
        {
            return;
        }

        float distanceTravelled = Vector3.Distance(startPosition, transform.position);
        // Debug.Log(collision.contacts[0].otherCollider.name);
        hitArmor = collision.contacts[0].otherCollider.GetComponent<Armor>();

        if (hitArmor == null)
        {
            Debug.Log("Hit non-Armored object");
            hasCollided = true;
            return;
        }


        // Time.timeScale = 0; 
        // Debug.Log("Armor thickness hit: " + hitArmor.KineticResistance);
        // Debug.Log("(old)At angle: " + Vector3.Angle(transform.forward, -collision.contacts[0].normal));
        // Debug.Log("At angle: " + Vector3.Angle(transform.forward, collision.contacts[0].normal));
        // Debug.Log("At angle 90-: " + (Vector3.Angle(transform.forward, collision.contacts[0].normal) - 90));

        // Time.timeScale = 0;
        remainingPen = CalculatePenetration(collision, distanceTravelled);
        if (remainingPen <= 0)
        {
            Debug.Log("Hit armor, no Penetration left");
            hasCollided = true;
            return;
        }

        hitArmor.RegisterDamage(CalculateDMG());
        Debug.Log("Damage dealt: " + CalculateDMG());
        hasCollided = true;
    }

    protected abstract float GetMaxPenetration();
    protected abstract float CalculateDMG();
    protected abstract float CalculatePenetration(Collision collision, float distanceTravelled);

}
