using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;
    public float acceleration = 10.0f;
    public float brakeStrenght = 10.0f;
    public float maxSpeed = 50.0f;
    public float rotationSpeed = 40.0f;

    bool isAlive;
    
    public GameObject barrel;
    public GameObject turret;
    public Transform aimPoint;
    public GameObject[] shells;
    int currentShell = 0;
    Rigidbody rb;



    bool IsGrounded(){
        Ray ray = new Ray(GetComponent<Collider>().transform.position - GetComponent<Collider>().bounds.extents.y * transform.up / 2, Vector3.down);
        //Debug.DrawRay(GetComponent<Collider>().transform.position - GetComponent<Collider>().bounds.extents.y * transform.up / 2, Vector3.down * 20, Color.red, 0);
        return Physics.Raycast(ray, GetComponent<Collider>().bounds.extents.y + 0.1f);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {   
        if(currentHealth <= 0 && isAlive)
            Die();
        turret.GetComponent<TurretRotaion>().RotateTowards(aimPoint);
        barrel.GetComponent<BarrelElevation>().Elevate(aimPoint);


    }
    void FixedUpdate(){
        // if(Input.GetAxis("Vertical") != 0)
        //     rb.AddForce(Vector3.forward * Input.GetAxis("Vertical")* acceleration, ForceMode.Acceleration);
    }

    public void TakeDamage(float damage){
        currentHealth -= damage;
    }
    
    public void Accelerate(float inputX){
        //Vector3 accel = transform.forward * inputX * acceleration;       
        //Debug.Log(transform.forward * inputX * acceleration);
        
        if(IsGrounded() && rb.velocity.magnitude < maxSpeed)
            rb.AddForce(transform.forward * inputX * acceleration, ForceMode.Acceleration);
    }

    public void Rotate(float inputX){
        if(IsGrounded())
            transform.Rotate(0, inputX * Time.deltaTime * rotationSpeed, 0);
        //Debug.Log(rb.velocity.magnitude);
        //rb.AddForce(transform.forward * rb.velocity.magnitude / 10 * Time.deltaTime, ForceMode.VelocityChange);
    }
    public void Brake(){
        //Debug.Log(rb.velocity);
        if(IsGrounded())
            rb.AddForce(-rb.velocity * brakeStrenght, ForceMode.Acceleration);
    }

    public void Fire(){
        barrel.transform.GetChild(0).GetComponent<FireProjectile>().Fire(shells[currentShell]);
    }

    public void SwitchBullet(){
        if(currentShell < shells.Length - 1)
            currentShell++;
        else
            currentShell = 0;
        Debug.Log("Selected shell: " + shells[currentShell].name);
    }
    
    void Die(){
        Debug.Log($"The tank \"{gameObject.name}\" has been destroyed");
        isAlive = false;
    }
    
}
