using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceEnemyScript : MonoBehaviour
{
    Rigidbody2D rigidBody;
    private int life = 10;

    public bool canShoot;
    public bool canMove = true;

    //public Transform attack;
    //public GameObject enemyBullet;
    GameObject scoreTextUI;

    [HideInInspector]
    public bool is_enemyBullet = false;

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
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
        if (damage >= life)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            scoreTextUI.GetComponent<ScoreScript>().Score += 20;
            //animation of damage
            //destroy(gameobject);
        }
        else
        {
            life -= damage;
        }
    }


    void StartShooting() {
        //GameObject bullet = Instantiate(enemyBullet, attack.position, Quaternion.identity);

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
