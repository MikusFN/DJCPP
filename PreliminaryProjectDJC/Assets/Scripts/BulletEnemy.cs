using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{

    public Transform gunPosition;
    public float speed;
    Vector3 _direction;
    bool isReady;
    public GameObject enemyBullet;

    void Awake() {

    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        isReady = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Move() {
    }

    public void setDirection(Vector3 direction) {
        _direction = direction;

        Debug.Log("bieen niño");


        Instantiate(enemyBullet, gunPosition.position, gunPosition.rotation);
        Instantiate(enemyBullet, gunPosition.position  - _direction, gunPosition.rotation);

        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));

        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1,1,0));

        if((transform.position.x < min.x) || (transform.position.x > max.x) ||
            (transform.position.y < min.y) || (transform.position.y > max.y)) {
                Destroy(gameObject);
            }
    }

}
