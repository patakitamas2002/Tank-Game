using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public Tank(Hull hull, Turret turret, Barrel barrel)
    {
        this.hull = hull;
        this.turret = turret;
        this.barrel = barrel;
    }
    public float maxHealth = 100;
    private float currentHealth;
    public float acceleration = 10.0f;
    public float brakeStrenght = 10.0f;
    public float maxSpeed = 50.0f;
    public float rotationSpeed = 40.0f;

    bool isAlive;

    public Hull hull;
    public Barrel barrel;
    public Turret turret;
    public Transform aimPoint;

    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        isAlive = true;



        Debug.Log("The tank \"" + gameObject.name + "\" has been created");

    }

    bool IsGrounded()
    {
        Ray ray = new Ray(GetComponent<Collider>().transform.position - GetComponent<Collider>().bounds.extents.y * transform.up / 2, Vector3.down);
        //Debug.DrawRay(GetComponent<Collider>().transform.position - GetComponent<Collider>().bounds.extents.y * transform.up / 2, Vector3.down * 20, Color.red, 0);
        return Physics.Raycast(ray, GetComponent<Collider>().bounds.extents.y + 0.1f);

    }
    // Start is called before the first frame update


    // Update is called once per frame
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

        if (IsGrounded() && rb.velocity.magnitude < maxSpeed)
            rb.AddForce(transform.forward * inputX * acceleration, ForceMode.Acceleration);
    }

    public void Rotate(float inputX)
    {
        if (IsGrounded())
            transform.Rotate(0, inputX * Time.deltaTime * rotationSpeed, 0);
        //Debug.Log(rb.velocity.magnitude);
        //rb.AddForce(transform.forward * rb.velocity.magnitude / 10 * Time.deltaTime, ForceMode.VelocityChange);
    }
    public void Brake()
    {
        //Debug.Log(rb.velocity);
        if (IsGrounded())
            rb.AddForce(-rb.velocity * brakeStrenght, ForceMode.Acceleration);
    }

    void Die()
    {
        Debug.Log($"The tank \"{gameObject.name}\" has been destroyed");
        isAlive = false;
        gameObject.SetActive(false);
    }


}
