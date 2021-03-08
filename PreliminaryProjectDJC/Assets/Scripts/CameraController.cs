using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject background;
    public float velocity;

    private BoxCollider2D[] cameraBoundsCollider;

    // Start is called before the first frame update
    void Awake()
    {
        velocity = 0.03f;

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(this.GetComponent<Camera>());

        cameraBoundsCollider = new BoxCollider2D[4];

        for (int i = 0; i < 4; i++)
        {
            cameraBoundsCollider[i] = this.gameObject.AddComponent<BoxCollider2D>();
            cameraBoundsCollider[i].offset = planes[i].normal * planes[i].distance;            
        }

        cameraBoundsCollider[3].size = new Vector2(cameraBoundsCollider[0].offset.x*2, 0.01f);
        cameraBoundsCollider[2].size = new Vector2(cameraBoundsCollider[0].offset.x*2, 0.01f);
        cameraBoundsCollider[1].size = new Vector2(0.01f, cameraBoundsCollider[2].offset.y*2);
        cameraBoundsCollider[0].size = new Vector2(0.01f, cameraBoundsCollider[2].offset.y*2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3((transform.position.x), transform.position.y + velocity, transform.position.z);        
    }
}
