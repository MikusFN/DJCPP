using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public GameObject impactEffect;

    float speed = 5f;

    private int damage = 20;
    private float timeOfLife = 0.0f;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        LifeTime();
    }


    void OnTriggerEnter2D(Collider2D hitInfo)
    {

        if (hitInfo.name == "Player")
        {
            PlayerController player;
            if (hitInfo.TryGetComponent<PlayerController>(out player))
            {
                player.TakeDamage(damage);
            }
            Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }


    private void LifeTime()
    {
        //contador de vida do projectil para o destroir 
        if (timeOfLife > 20.0f)
        {
            timeOfLife = 0.0f;
            Destroy(gameObject);
        }
        timeOfLife += Time.fixedDeltaTime;
    }



}
