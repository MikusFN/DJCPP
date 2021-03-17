using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{

    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        Invoke ("FireEnemyBullet", 0.3f);
        Invoke ("FireEnemyBullet", 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FireEnemyBullet() {

        GameObject player = GameObject.Find ("Player");

        if (player != null && isInside()) {

            Vector3 direction = player.transform.position - transform.position;

            GameObject go = Instantiate(bullet, transform.position, transform.rotation);
            go.GetComponent<Rigidbody2D>().velocity = (direction - go.transform.position).normalized * 2f;

        }
    }

    bool isInside() {
        //verify if the enemy is inside the screen

        return true;
    }
}
