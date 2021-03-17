using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{

    public GameObject EnemyBullets;
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

            GameObject bullet = (GameObject)Instantiate(EnemyBullets);

            bullet.transform.position = transform.position;

            Vector3 direction = player.transform.position - bullet.transform.position;

            Debug.Log("aqui bien");

            bullet.GetComponent<BulletEnemy>().setDirection(direction);
        }
    }
}
