using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAfterTel : MonoBehaviour
{
    //public float speed = 10f;
    public int damage = 100;
    //public Rigidbody2D rb;


    private int currentDamage = 100;
    //public GameObject impactEffect;


    private float timeOfLife = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = -transform.up;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.localScale = new Vector3(1.0f + timeOfLife * 2f, 1.0f + timeOfLife * 2f, 1.0f);
        LifeTime();
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        GameObstacle obstacle;
        SpaceEnemyScript spaceEnemy;
        EnemyScript enemy;

        if (hitInfo.TryGetComponent<GameObstacle>(out obstacle))
        {
            obstacle.takeDamage(currentDamage);
            GetComponentInParent<WeaponBehaviour>().GetComponent<PlayerController>().Score += currentDamage;
        }

        else if (hitInfo.TryGetComponent<SpaceEnemyScript>(out spaceEnemy))
        {
            spaceEnemy.takeDamage(currentDamage);
            GetComponentInParent<WeaponBehaviour>().GetComponent<PlayerController>().Score += currentDamage;
        }

        else if (hitInfo.TryGetComponent<EnemyScript>(out enemy))
        {
            enemy.takeDamage(currentDamage);
            GetComponentInParent<WeaponBehaviour>().GetComponent<PlayerController>().Score += currentDamage;
        }

        //if (hitInfo.name != "Player" && hitInfo.tag != this.tag)
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
        if (timeOfLife > 5.0f)
        {
            timeOfLife = 0.0f;
            Destroy(gameObject);
        }
        timeOfLife += Time.fixedDeltaTime;
    }
}
