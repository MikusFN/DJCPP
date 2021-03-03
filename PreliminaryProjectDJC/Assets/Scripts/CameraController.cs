using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject background;
    public float velocity;
    // Start is called before the first frame update
    void Start()
    {
        velocity = 0.03f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3((transform.position.x), transform.position.y+velocity, transform.position.z);
        //background.transform.position = new Vector3((background.transform.position.x + velocity),
        //    background.transform.position.y,
        //    background.transform.position.z);
    }
}
