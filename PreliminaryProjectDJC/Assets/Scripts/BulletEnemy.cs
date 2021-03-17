using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{

    float speed;
    Vector3 _direction;
    bool isReady;

    void Awake() {
        speed = 5f;
        isReady = false;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void setDirection(Vector3 direction) {

        Debug.Log("Posicion JUGADOR:" + direction);

        _direction = direction.normalized;

        Debug.Log("Direccion:" + _direction);

        isReady = true;

        Debug.Log("isReady:" + isReady);

    }

    // Update is called once per frame
    void Update()
    {

        if (isReady) {


            Vector3 position = transform.position;

            Debug.Log("desde:" + position);

            Debug.Log("sumado a:" + _direction);

            position += _direction * speed * Time.deltaTime;
            transform.position = position;

            Debug.Log("hacia:" + position);

            Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
            Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1,1,0));

            if((transform.position.x < min.x) || (transform.position.x > max.x) ||
             (transform.position.y < min.y) || (transform.position.y < max.y)) {

                 Destroy(gameObject);
             }
        }
    }


}
