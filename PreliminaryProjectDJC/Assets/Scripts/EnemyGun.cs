using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{

    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        Invoke ("FireEnemyBullet", 1f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FireEnemyBullet() {

        GameObject player = GameObject.Find ("Player");

        if (player != null) {
            
            GameObject bulletObject = (GameObject)Instantiate(bullet);

            bulletObject.transform.position = transform.position;

            Vector3 direction = player.transform.position - bulletObject.transform.position;

            bullet.GetComponent<BulletEnemy>().setDirection(direction);
        }
    }
}
