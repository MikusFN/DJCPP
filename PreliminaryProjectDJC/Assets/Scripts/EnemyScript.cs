using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public float speed;
    Rigidbody2D rigidBody;
    private int life = 10;
    private bool isAlive = true;

    public bool canShoot;
    public bool canMove = true;

    public Transform attack;
    public GameObject enemyBullet;

    private bool moveLeft = false;
    private bool moveRight = false;

    [HideInInspector]
    public bool is_enemyBullet = false;

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if( is_enemyBullet) {
            speed *= -1f;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        WhereAreYou();
        Move();
    }

    void Move() {

        if(canMove) {
            if(moveLeft) {

                transform.Translate(-Time.deltaTime * speed, 0,0);
            }
            else if(moveRight) {
                transform.Translate(Time.deltaTime * speed, 0,0);
            }

        else {
            transform.Translate(Time.deltaTime * speed * Random.Range(-1,1), 0,0);
        }
        }
    }

    void WhereAreYou() {
            if(rigidBody.position.x >= 1) {
                moveLeft = true;
            }

            else if(rigidBody.position.x <= -1) {
                moveRight = true;
            }
    }

    void StartShooting() {
        GameObject bullet = Instantiate(enemyBullet, attack.position, Quaternion.identity);
        bullet.GetComponent<BulletEnemy>().Move();

        Invoke("StartShooting", Random.Range(1f, 3f));
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
    
        PlayerController player;
        if(hitInfo.TryGetComponent<PlayerController>(out player))
        {
            Debug.Log("OSTION");
            player.TakeDamage(50);
        }
    }

}
