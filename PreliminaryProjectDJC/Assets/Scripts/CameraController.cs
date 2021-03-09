using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject background;
    public float velocityRate;
    public float velovityMax;

    private BoxCollider2D[] cameraBoundsCollider;
    private bool inColision = false;
    private float velocity;
    private float velocityMin = 0.03f;


    // Start is called before the first frame update
    void Awake()
    {
        velocity = velocityMin;

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(this.GetComponent<Camera>());

        cameraBoundsCollider = new BoxCollider2D[4];

        for (int i = 0; i < 4; i++)
        {
            cameraBoundsCollider[i] = this.gameObject.AddComponent<BoxCollider2D>();
            cameraBoundsCollider[i].offset = planes[i].normal * planes[i].distance;
            //cameraBoundsCollider[i].isTrigger = true;
        }

        cameraBoundsCollider[3].size = new Vector2(cameraBoundsCollider[0].offset.x * 2, 0.01f);
        cameraBoundsCollider[2].size = new Vector2(cameraBoundsCollider[0].offset.x * 2, 0.01f);
        cameraBoundsCollider[1].size = new Vector2(0.01f, cameraBoundsCollider[2].offset.y * 2);
        cameraBoundsCollider[0].size = new Vector2(0.01f, cameraBoundsCollider[2].offset.y * 2);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerController player;
        if (collision.otherCollider.bounds == cameraBoundsCollider[2].bounds
            &&
            collision.collider.TryGetComponent<PlayerController>(out player))
        {
            inColision = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerController player;
        if (collision.otherCollider.bounds == cameraBoundsCollider[2].bounds
            &&
            collision.collider.TryGetComponent<PlayerController>(out player))
        {
            inColision = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        VelocityControl();
        transform.position = new Vector3((transform.position.x), transform.position.y + velocity, transform.position.z);
    }

    private void VelocityControl()
    {
        if (inColision)
        {
            if (velocity < velovityMax)
            {
                velocity += velocityRate * Time.deltaTime;
            }
        }
        else
        {
            if (velocity > velocityMin)
            {
                velocity -= velocityRate * Time.deltaTime;
            }
        }

        if (velocity > velocityMin && velocity < velovityMax)
            velocity += Input.GetAxis("Vertical") * Time.deltaTime;
        else if (velocity < velocityMin)
        {
            velocity = velocityMin;
        }

    }

    public bool IsInsideScreen(Bounds bounds)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(GetComponent<Camera>());
        return GeometryUtility.TestPlanesAABB(planes, bounds);
    }
}
