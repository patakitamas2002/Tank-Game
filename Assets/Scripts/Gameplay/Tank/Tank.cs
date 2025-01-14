using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tank : MonoBehaviour
{
    // public Tank(Hull hull, Turret turret, Barrel barrel)
    // {
    //     this.hull = hull;
    //     this.turret = turret;
    //     this.barrel = barrel;
    //     // this.maxHealth = hull.Health;
    //     // this.maxHealth += turret.Health;
    //     // this.maxHealth += barrel.Health;
    // }

    public float maxHealth { get; private set; }
    public float currentHealth { get; private set; }


    public Hull hull;
    public Barrel barrel;
    public Turret turret;
    public Transform aimPoint;

    private BoxCollider boxCollider;
    private const float ton = 1000;

    public float accelSpeed { get; private set; }
    public float rotationSpeed { get; private set; }

    private float sidewaysFrictionFactor = 0.04f;
    private bool grounded = false;
    public Rigidbody rb { get; private set; }

    public static GameObject CreateTank(GameObject hull, GameObject turret, GameObject barrel, Transform transform)
    {
        Tank newTank = new GameObject("Tank", typeof(Tank), typeof(Rigidbody), typeof(BoxCollider)).GetComponent<Tank>();
        newTank.transform.position = transform.position;

        newTank.hull = Instantiate(hull.GetComponent<Hull>(), newTank.transform);
        newTank.turret = Instantiate(turret.GetComponent<Turret>(), newTank.hull.transform.GetChild(0).transform);
        newTank.barrel = Instantiate(barrel.GetComponent<Barrel>(), newTank.turret.transform.GetChild(0).transform);

        return newTank.gameObject;
    }

    void Start()
    {

        Debug.Log(hull.GetComponentInChildren<Renderer>().bounds.size);
        GetComponent<BoxCollider>().size = hull.transform.GetChild(3).GetComponent<Renderer>().bounds.size;
        SetStats();
        Debug.Log("The tank \"" + gameObject.name + "\" has been created");
    }

    void FixedUpdate()
    {
        grounded = IsGrounded();
        if (grounded)
            SidewaysFriction();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(boxCollider.bounds.center, 1.5f);
    }
    private void SetStats()
    {
        boxCollider = GetComponent<BoxCollider>();

        gameObject.tag = "Player";
        gameObject.layer = LayerMask.NameToLayer("CollisionBox");

        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -2, 0);


        maxHealth = this.hull.stats.Health + this.barrel.stats.Health + this.turret.stats.Health;
        rb.mass = hull.stats.Weight + barrel.stats.Weight + turret.stats.Weight;
        currentHealth = maxHealth;


        accelSpeed = hull.stats.Horsepower / (hull.stats.Weight / ton) * 20;
        rotationSpeed = hull.stats.Horsepower / (hull.stats.Weight / ton) * 2;

    }
    private void SidewaysFriction()
    {
        Vector3 rightDirection = transform.right;

        // Project the Rigidbody velocity onto the right direction
        float sidewaysSpeed = Vector3.Dot(rb.velocity, rightDirection);

        // Create a velocity vector to counteract the sideways motion
        Vector3 sidewaysVelocity = rightDirection * sidewaysSpeed;

        // Apply friction to reduce the sideways velocity
        Vector3 newVelocity = rb.velocity - sidewaysVelocity * sidewaysFrictionFactor;

        // Update the Rigidbody's velocity
        rb.velocity = newVelocity;
    }
    bool IsGrounded()
    {
        return Physics.CheckSphere(boxCollider.bounds.center, 1.5f, LayerMask.GetMask("Terrain"));
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Accelerate(float inputX)
    {
        //Vector3 accel = transform.forward * inputX * acceleration;       
        //Debug.Log(transform.forward * inputX * acceleration);

        if (grounded && rb.velocity.magnitude < hull.stats.MaxSpeed)
            rb.AddForce(transform.forward * inputX * accelSpeed, ForceMode.Acceleration);

        // Debug.Log(transform.forward * inputX * accelSpeed * Time.fixedDeltaTime);
        //        Debug.Log(transform.forward * hull.stats.Horsepower * Time.fixedDeltaTime / (hull.stats.Weight / ton));
        // rb.AddForce(100 * inputX * transform.forward, ForceMode.Acceleration);
    }

    public void Rotate(float inputX)
    {
        if (!grounded)
            return;
        if (rb.velocity.magnitude < 2)
        {
            transform.Rotate(0, inputX * rotationSpeed, 0);
            return;
        }
        transform.Rotate(0, inputX * rotationSpeed * (float)Math.Pow(rb.velocity.magnitude, 0.2), 0);
        // rb.AddForce(100 * inputX * transform.right, ForceMode.Acceleration);
        //Debug.Log(rb.velocity.magnitude);
        //rb.AddForce(transform.forward * rb.velocity.magnitude / 10 * Time.deltaTime, ForceMode.VelocityChange);
    }
    public void Brake(float inputX)
    {
        //Debug.Log(rb.velocity);
        if (grounded)
            rb.AddForce(-rb.velocity * inputX, ForceMode.Acceleration);
    }

    void Die()
    {
        Debug.Log($"The tank \"{gameObject.name}\" has been destroyed");


        turret.gameObject.SetActive(false);
        GameObject faketurret = Instantiate(turret.gameObject, turret.transform);
        Instantiate(barrel.GetComponent<Barrel>(), faketurret.transform.GetChild(0).transform);
        Rigidbody fakeRb = faketurret.AddComponent<Rigidbody>();
        fakeRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }
}
