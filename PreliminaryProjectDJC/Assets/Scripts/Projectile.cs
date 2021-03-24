using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 20;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    GameObject scoreTextUI;


    private int currentDamage = 10;
    //public GameObject impactEffect;


    private float timeOfLife = 0.0f;

    // Use this for initialization
    void Start()
    {
        scoreTextUI = GameObject.FindGameObjectWithTag("ScoreTextTag");

        //rb.velocity = (GetComponentInParent<WeaponBehaviour>().GetComponentInParent<Transform>().position-transform.position).normalized *speed;
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


        GameObstacle obstacle;
        SpaceEnemyScript spaceEnemy;
        EnemyScript enemy;

        if (hitInfo.TryGetComponent<GameObstacle>(out obstacle))
        {
            obstacle.takeDamage(currentDamage);
            GetComponentInParent<WeaponBehaviour>().GetComponent<PlayerController>().Score += currentDamage;
            Instantiate(impactEffect, transform.position, transform.rotation);
        }

        else if (hitInfo.TryGetComponent<SpaceEnemyScript>(out spaceEnemy))
        {
            spaceEnemy.takeDamage(currentDamage);
            GetComponentInParent<WeaponBehaviour>().GetComponent<PlayerController>().Score += currentDamage;
            Instantiate(impactEffect, transform.position, transform.rotation);
        }

        else if (hitInfo.TryGetComponent<EnemyScript>(out enemy))
        {
            enemy.takeDamage(currentDamage);
            GetComponentInParent<WeaponBehaviour>().GetComponent<PlayerController>().Score += currentDamage;
            Instantiate(impactEffect, transform.position, transform.rotation);
        }



        if (hitInfo.name != "Player" && hitInfo.tag != this.tag)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        //Instantiate(impactEffect, transform.position, transform.rotation);
        //Debug.Log("destroy");
    }

    private void LifeTime()
    {
        //contador de vida do projectil para o destroir 
        if (timeOfLife > 15.0f)
        {
            timeOfLife = 0.0f;
            Destroy(gameObject);
        }
        timeOfLife += Time.fixedDeltaTime;
    }
}
