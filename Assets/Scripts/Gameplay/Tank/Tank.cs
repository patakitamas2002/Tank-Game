using System;
using System.Collections;
using System.Collections.Generic;
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
    private float currentHealth;

    bool isAlive;

    public Hull hull;
    public Barrel barrel;
    public Turret turret;
    public Transform aimPoint;


    Rigidbody rb;


    void Start()
    {
        gameObject.tag = "CollisionBox";
        aimPoint = transform.GetChild(0);
        rb = GetComponent<Rigidbody>();

        isAlive = true;

        GameObject hullObject = Instantiate(GameOptions.hull.Model, transform);
        GameObject turretObject = Instantiate(GameOptions.turret.Model, hullObject.transform.GetChild(0).transform);
        GameObject barrelObject = Instantiate(GameOptions.barrel.Model, turretObject.transform.GetChild(0).transform);

        hull = hullObject.GetComponent<Hull>();
        barrel = barrelObject.GetComponent<Barrel>();
        turret = turretObject.GetComponent<Turret>();


        maxHealth = this.hull.stats.Health + this.barrel.stats.Health + this.turret.stats.Health;
        rb.mass = hull.stats.Weight + barrel.stats.Weight + turret.stats.Weight;

        currentHealth = maxHealth;

        Debug.Log("The tank \"" + gameObject.name + "\" has been created");
    }

    bool IsGrounded()
    {
        Ray ray = new Ray(GetComponent<Collider>().transform.position - GetComponent<Collider>().bounds.extents.y * transform.up / 2, Vector3.down);
        //Debug.DrawRay(GetComponent<Collider>().transform.position - GetComponent<Collider>().bounds.extents.y * transform.up / 2, Vector3.down * 20, Color.red, 0);
        return Physics.Raycast(ray, GetComponent<Collider>().bounds.extents.y + 0.1f);

    }

    void Update()
    {
        turret.GetComponent<Turret>().RotateTowards(aimPoint);
        barrel.GetComponent<Barrel>().Elevate(aimPoint);
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
            rb.AddForce(transform.forward * inputX * hull.stats.Horsepower / hull.stats.Weight, ForceMode.Acceleration);
    }

    public void Rotate(float inputX)
    {
        if (IsGrounded())
            transform.Rotate(0, inputX * Time.deltaTime * hull.stats.Horsepower / hull.stats.Weight * 2, 0);
        //Debug.Log(rb.velocity.magnitude);
        //rb.AddForce(transform.forward * rb.velocity.magnitude / 10 * Time.deltaTime, ForceMode.VelocityChange);
    }
    public void Brake()
    {
        //Debug.Log(rb.velocity);
        if (IsGrounded())
            rb.AddForce(-rb.velocity, ForceMode.Acceleration);
    }

    void Die()
    {
        Debug.Log($"The tank \"{gameObject.name}\" has been destroyed");
        isAlive = false;
        gameObject.SetActive(false);
    }
}
