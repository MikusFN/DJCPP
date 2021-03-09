using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObstacle : MonoBehaviour
{
    public int Life { get { return life; } set { life = value; } }
    public int Damage { get { return damage; } }

    public int InitialLife { get => initialLife; }

    private int life = 10;
    private int initialLife = 10;
    private int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<CapsuleCollider2D>().enabled = false;
        //GetComponent<Rigidbody2D>().AddForce(-transform.up.normalized * speed * Time.deltaTime, ForceMode2D.Force);
    }

    // Update is called once per frame

    public void takeDamage(int damage)
    {
        if (damage >= life)
        {
            GetComponent<CapsuleCollider2D>().isTrigger = true;
            GetComponent<SpriteRenderer>().enabled = false;
            //animation of damage
            //destroy(gameobject);
        }
        else
        {
            life -= damage;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Player")
        //|| collision.gameObject.tag == "Enemy")
        PlayerController player;
        if(collision.TryGetComponent<PlayerController>(out player))
        {
            player.TakeDamage(damage);
        }
    }
}
