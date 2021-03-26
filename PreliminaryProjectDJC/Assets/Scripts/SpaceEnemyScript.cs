using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceEnemyScript : MonoBehaviour
{
    Rigidbody2D rigidBody;
    private int life;
    private int initialLife = 20;
    private int damage = 55;


    public bool canShoot;
    public bool canMove = true;
    public GameObject deathEffect;
    public GameObject thruster;
    public GameObject enemyGun;


    //public Transform attack;
    //public GameObject enemyBullet;
    GameObject scoreTextUI;

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
        //StartShooting();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void takeDamage(int damage)
    {
        if (damage >= Life)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            scoreTextUI.GetComponent<ScoreScript>().Score += 20;

            //animation of damage
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            thruster.SetActive(false);
            enemyGun.SetActive(false);
            //Destroy(gameObject);

        }
        else
        {
            Life -= damage;
        }
    }


    void StartShooting() {
        //GameObject bullet = Instantiate(enemyBullet, attack.position, Quaternion.identity);

        //Invoke("StartShooting", Random.Range(1f, 3f));
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
