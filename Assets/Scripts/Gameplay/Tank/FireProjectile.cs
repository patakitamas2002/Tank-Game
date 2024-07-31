using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{

    public float reloadTime = 10f;
    private float reload = 0;
    // Update is called once per frame
    void Update()
    {

        if (reload > 0f)
        {
            reload -= Time.deltaTime;
        }

    }
    public void Fire(GameObject bullet)
    {
        if (reload <= 0f)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            reload = reloadTime;

        }
    }
}
