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

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.localScale = new Vector3(1.0f + timeOfLife * 1.5f, 1.0f + timeOfLife * 1.5f, 1.0f);
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
