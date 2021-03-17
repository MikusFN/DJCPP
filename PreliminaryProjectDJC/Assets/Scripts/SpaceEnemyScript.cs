using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceEnemyScript : MonoBehaviour
{
    Rigidbody2D rigidBody;
    private int life = 10;
    private bool isAlive = true;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3 (0,0,0));

        if (transform.position.y < min.y) {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
    
        PlayerController player;
        if(hitInfo.TryGetComponent<PlayerController>(out player))
        {
            player.TakeDamage(55);
        }
    }
}
