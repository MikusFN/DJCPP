using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;

    private int currentDamage = 10;
    //public GameObject impactEffect;


    private float timeOfLife = 0.0f;

    // Use this for initialization
    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    private void Update()
    {
        LifeTime();
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //Enemy enemy = hitInfo.GetComponent<Enemy>();
        //if (enemy != null)
        //{
        //	enemy.TakeDamage(damage);
        //}

        //Instantiate(impactEffect, transform.position, transform.rotation);

        GameObstacle obstacle;
        if (hitInfo.TryGetComponent<GameObstacle>(out obstacle))
        {
            obstacle.takeDamage(currentDamage);
        }
        
        if (hitInfo.name != "Player")
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        //Instantiate(impactEffect, transform.position, transform.rotation);
        //Debug.Log("destroy");
    }

    private void LifeTime()
    {
        //contador de vida do projectil para o destroir 
        if (timeOfLife > 10.0f)
        {
            timeOfLife = 0.0f;
            Destroy(gameObject);
        }
        timeOfLife += Time.fixedDeltaTime;
    }
}
