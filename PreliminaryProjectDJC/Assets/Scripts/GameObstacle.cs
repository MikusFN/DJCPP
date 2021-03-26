using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObstacle : MonoBehaviour
{
    public int Life { get { return life; } set { life = value; } }
    public int mySize = 0;
    public bool hasLifeSpan = false;
    public GameObject deathEffect;

    public int InitialLife { get => initialLife; }
    public int Damage { get => damage; set => damage = value; }

    private int life = 10;
    private int initialLife = 10;
    private int damage = 10;
    private float timeOfLife = 0.0f;
    GameObject scoreTextUI;


    // Start is called before the first frame update
    void Start()
    {

        scoreTextUI = GameObject.FindGameObjectWithTag("ScoreTextTag");
        SpriteRenderer sr;

        if (TryGetComponent<SpriteRenderer>(out sr))
        {
            mySize = (int)((sr.sprite.bounds.size.x) / 0.32f);
            initialLife = (int)sr.sprite.bounds.size.x * 3;
        }
        //GetComponent<CapsuleCollider2D>().enabled = false;
        //GetComponent<Rigidbody2D>().AddForce(-transform.up.normalized * speed * Time.deltaTime, ForceMode2D.Force);
    }

    // Update is called once per frame
    private void Update()
    {
        if (hasLifeSpan)
        {
            LifeTime();
        }
    }

    public void takeDamage(int damage)
    {
        if (damage >= life)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            scoreTextUI.GetComponent<ScoreScript>().Score += 5;
            //animation of damage
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            //destroy(gameobject);
        }
        else
        {
            life -= damage;
        }
    }

    public void ExplodeOnCollision(int damage, Transform currentPos, GameObject explodingPrefab)
    {
        if (mySize > 0)
        {
            int randomMul = Random.Range(5, 10);
            Vector3[] aux = CircularPosition(currentPos, mySize * randomMul);
            int step = (int)(360 / (float)mySize * randomMul);

            Rigidbody2D rb2D;
            GameObject go;

            for (int i = 0; i < (mySize * randomMul); i++)
            {
                step *= Random.Range(-((int)step / 4), ((int)step / 4));
                go = Instantiate(explodingPrefab, aux[i] + transform.position, Quaternion.Euler(0, 0, (i + 1) * step));
                go.transform.localScale *= Random.Range(0.2f, 2.0f);
                go.GetComponent<GameObstacle>().hasLifeSpan = true;
                if (go.TryGetComponent<Rigidbody2D>(out rb2D))
                {
                    //center point calculate to the outer points already in place
                    rb2D.velocity = ((aux[i] + transform.position) - transform.position).normalized * Random.Range(200.0f, 400.0f);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Player")
        //|| collision.gameObject.tag == "Enemy")
        PlayerController player;
        SpaceEnemyScript ses;
        EnemyScript es;
        if (collision.TryGetComponent<PlayerController>(out player))
        {
            player.TakeDamage(Damage);
        }
        else if (collision.TryGetComponent<SpaceEnemyScript>(out ses))
        {
            if (this.hasLifeSpan)
                ses.takeDamage(Damage);
        }
        else if (collision.TryGetComponent<EnemyScript>(out es))
        {
            if (this.hasLifeSpan)
                es.takeDamage(Damage);
        }
    }

    public Vector3[] CircularPosition(Transform currentPos, int sizeOfPositions)
    {
        Vector3[] positions = new Vector3[sizeOfPositions];
        int step = (int)(360 / (float)sizeOfPositions);

        for (int i = 0; i < sizeOfPositions; i++)
        {
            step *= Random.Range(-((int)step / 4), ((int)step / 4));
            positions[i] = Quaternion.Euler(0, 0, (i + 1) * step) * (currentPos.position + currentPos.forward).normalized * .1f;
            positions[i].z = 10;
        }

        return positions;
    }

    private void LifeTime()
    {
        //contador de vida do projectil para o destroir 
        if (timeOfLife > 20.0f)
        {
            timeOfLife = 0.0f;
            Destroy(gameObject);
        }
        timeOfLife += Time.fixedDeltaTime;
    }
}
