using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceEnemyScript : MonoBehaviour
{
    Rigidbody2D rigidBody;
    private int life = 10;
    private bool isAlive = true;

    public bool canShoot;
    public bool canMove = true;

    public Transform attack;
    public GameObject enemyBullet;

    [HideInInspector]
    public bool is_enemyBullet = false;

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartShooting();
    }

    // Update is called once per frame
    void Update()
    {
    }


    void StartShooting() {
        GameObject bullet = Instantiate(enemyBullet, attack.position, Quaternion.identity);

        Invoke("StartShooting", Random.Range(1f, 3f));
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
