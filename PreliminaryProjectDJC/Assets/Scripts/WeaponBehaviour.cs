using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float rateOfFire = 1.0f;

    private float timeOfLife = 0.0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (RateFire())
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Instantiate(bulletPrefab, firePoint.position - new Vector3(.1f, 0, 0), firePoint.rotation);
            Instantiate(bulletPrefab, firePoint.position + new Vector3(.1f, 0, 0), firePoint.rotation);
        }
    }

    private bool RateFire()
    {
        //contador de vida do projectil para o destroir 
        if (timeOfLife > rateOfFire)
        {
            timeOfLife = 0.0f;
            return true;
        }
        timeOfLife += Time.fixedDeltaTime;
        return false;
    }
}
