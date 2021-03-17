using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{

    float speed = 5f;

    private int damage = 20;

    void Awake() {
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        
        if (hitInfo.name == "Player") {
        PlayerController player;
            if(hitInfo.TryGetComponent<PlayerController>(out player))
            {
                player.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }



}
