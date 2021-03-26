using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public GameObject deathEffect;
    public float speed;
    Rigidbody2D rigidBody;
    private int life;
    private int initialLife = 20;
    private int damage = 50;

    public bool canShoot;
    public bool canMove = true;

    GameObject scoreTextUI;


    private bool moveLeft = false;
    private bool moveRight = false;

    [HideInInspector]
    public bool is_enemyBullet = false;

    public int Life { get => life; set => life = value; }
    public int InitialLife { get => initialLife; set => initialLife = value; }
    public int Damage { get => damage; set => damage = value; }

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        life = InitialLife;
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
        Move();
    }

    void Move() {


        GameObject player = GameObject.Find("Player");

        if (player != null)
        {

            Vector3 direction = player.transform.position - transform.position;

            transform.Translate(Time.deltaTime * speed * direction.x, 0, 0);

        }
    }


    public void takeDamage(int damage)
    {
        if (damage >= Life)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            scoreTextUI.GetComponent<ScoreScript>().Score += 10;

            //animation of damage
            Instantiate(deathEffect, transform.position, Quaternion.identity);

            //Destroy(gameObject);
        }
        else
        {
            Life -= damage;
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
    
        PlayerController player;
        if(hitInfo.TryGetComponent<PlayerController>(out player))
        {
            player.TakeDamage(Damage);
        }
    }

}
