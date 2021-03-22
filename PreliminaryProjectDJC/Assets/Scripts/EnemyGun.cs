using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{

    public GameObject bullet;
    public CameraController MainCamera;
    public SpaceEnemyScript ses;

    private float coolDownTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("FireEnemyBullet", 0.5f, 0.5f);
        //FireEnemyBullet();
    }

    // Update is called once per frame
    void Update()
    {       
        //add cool down timer 
        if(MainCamera.IsInsideScreen(ses.GetComponent<SpriteRenderer>().bounds))
        {
            if (coolDownTime > 1.0f)
            {
                FireEnemyBullet();
                coolDownTime = 0.0f;
            }
        }
        
        coolDownTime += Time.deltaTime;
    }

    void FireEnemyBullet()
    {

        GameObject player = GameObject.Find("Player");

        if (player != null)
        {

            Vector3 direction = player.transform.position - transform.position;

            GameObject go = Instantiate(bullet, transform.position, transform.rotation);
            go.GetComponent<Rigidbody2D>().velocity = (direction).normalized;

        }
    }
}
