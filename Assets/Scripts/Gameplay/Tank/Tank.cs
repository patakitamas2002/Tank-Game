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

    Rigidbody rb;

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

        // aimPoint = Instantiate(new GameObject("Aimpoint"), transform).transform;

        Debug.Log(hull.GetComponentInChildren<Renderer>().bounds.size);

        GetComponent<BoxCollider>().size = hull.transform.GetChild(3).GetComponent<Renderer>().bounds.size;
        boxCollider = GetComponent<BoxCollider>();
        gameObject.tag = "CollisionBox";
        rb = GetComponent<Rigidbody>();


        // GameObject hullObject = Instantiate(hull.stats.Model, transform);
        // GameObject turretObject = Instantiate(turret.stats.Model, hullObject.transform.GetChild(0).transform);
        // GameObject barrelObject = Instantiate(barrel.stats.Model, turretObject.transform.GetChild(0).transform);

        // hull = hullObject.GetComponent<Hull>();
        // barrel = barrelObject.GetComponent<Barrel>();
        // turret = turretObject.GetComponent<Turret>();


        maxHealth = this.hull.stats.Health + this.barrel.stats.Health + this.turret.stats.Health;
        rb.mass = hull.stats.Weight + barrel.stats.Weight + turret.stats.Weight;

        currentHealth = maxHealth;

        Debug.Log("The tank \"" + gameObject.name + "\" has been created");
    }

    // void FixedUpdate()
    // {
    //     if (IsGrounded()) Debug.Log("Grounded");
    // }
    bool IsGrounded()
    {
        Ray ray = new Ray(boxCollider.transform.position - boxCollider.bounds.extents.y * transform.up / 2, Vector3.down);
        Debug.DrawRay(boxCollider.transform.position - (boxCollider.bounds.extents.y * transform.up / 2), Vector3.down, Color.red);
        // Debug.Log(Physics.Raycast(ray, boxCollider.bounds.extents.y + 0.6f));
        // Debug.Log(Physics.Raycast(ray, GetComponent<BoxCollider>().bounds.extents.y + 0.1f));
        return Physics.Raycast(ray, boxCollider.bounds.extents.y + 0.6f);

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

        if (IsGrounded() && rb.velocity.magnitude < hull.stats.MaxSpeed)
            rb.AddForce(transform.forward * inputX * hull.stats.Horsepower / (hull.stats.Weight / ton), ForceMode.Acceleration);
        //        Debug.Log(transform.forward * hull.stats.Horsepower * Time.fixedDeltaTime / (hull.stats.Weight / ton));
        // rb.AddForce(100 * inputX * transform.forward, ForceMode.Acceleration);
    }

    public void Rotate(float inputX)
    {
        if (IsGrounded())
            transform.Rotate(0, inputX * hull.stats.Horsepower * Time.fixedDeltaTime / (hull.stats.Weight / ton), 0);
        // rb.AddForce(100 * inputX * transform.right, ForceMode.Acceleration);
        //Debug.Log(rb.velocity.magnitude);
        //rb.AddForce(transform.forward * rb.velocity.magnitude / 10 * Time.deltaTime, ForceMode.VelocityChange);
    }
    public void Brake(float inputX)
    {
        //Debug.Log(rb.velocity);
        if (IsGrounded())
            rb.AddForce(-rb.velocity * inputX, ForceMode.Acceleration);
    }

    void Die()
    {
        Debug.Log($"The tank \"{gameObject.name}\" has been destroyed");
        gameObject.SetActive(false);
    }
}
