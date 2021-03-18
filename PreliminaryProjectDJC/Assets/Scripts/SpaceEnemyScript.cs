using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceEnemyScript : MonoBehaviour
{
    Rigidbody2D rigidBody;
    private int life = 9;
    private bool isAlive = true;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

    }


    private void OnTriggerEnter2D(Collider2D hitInfo)
    {

        PlayerController player;
        if (hitInfo.TryGetComponent<PlayerController>(out player))
        {
            player.TakeDamage(55);
        }

        else if (hitInfo.name == "Projectile")
        {
            Destroy(gameObject);
        }
    }

}
