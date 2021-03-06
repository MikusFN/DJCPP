using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerProjectile : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public GameObject explodingPrefab;

    private int currentDamage = 60;
    //public GameObject impactEffect;


    private float timeOfLife = 0.0f;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    private void Update()
    {
        LifeTime();
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        SpaceEnemyScript spaceEnemy;
        EnemyScript enemy;        

        if (hitInfo.TryGetComponent<SpaceEnemyScript>(out spaceEnemy))
        {
            spaceEnemy.takeDamage(currentDamage);
            GetComponentInParent<WeaponBehaviour>().GetComponent<PlayerController>().Score += currentDamage;
        }

        else if (hitInfo.TryGetComponent<EnemyScript>(out enemy))
        {
            enemy.takeDamage(currentDamage);
            GetComponentInParent<WeaponBehaviour>().GetComponent<PlayerController>().Score += currentDamage;
        }

        GameObstacle obstacle;
        if (hitInfo.TryGetComponent<GameObstacle>(out obstacle))
        {
            obstacle.ExplodeOnCollision(currentDamage, hitInfo.transform, explodingPrefab);
            obstacle.takeDamage(damage);
            GetComponentInParent<WeaponBehaviour>().GetComponent<PlayerController>().Score += currentDamage;
            Destroy(gameObject);

        }

        //if (hitInfo.name != "Player" && hitInfo.tag != "Projectile" && hitInfo.tag != "MainCamera")
        //    Destroy(gameObject);
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
