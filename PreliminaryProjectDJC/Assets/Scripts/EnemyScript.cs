using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public float speed;
    Rigidbody2D rigidBody;
    private int life = 10;

    public bool canShoot;
    public bool canMove = true;

    GameObject scoreTextUI;


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
        scoreTextUI = GameObject.FindGameObjectWithTag ("ScoreTextTag");

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


    public void takeDamage(int damage)
    {
        if (damage >= life)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            scoreTextUI.GetComponent<ScoreScript>().Score += 10;
            //animation of damage
            //destroy(gameobject);
        }
        else
        {
            life -= damage;
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
    
        PlayerController player;
        if(hitInfo.TryGetComponent<PlayerController>(out player))
        {
            player.TakeDamage(50);
        }
    }

}
