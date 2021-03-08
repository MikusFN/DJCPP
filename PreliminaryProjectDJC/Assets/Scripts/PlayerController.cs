using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;


    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;


    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(float move, bool axis, Vector2 mousePos)
    {
        Vector3 targetVelocity = m_Velocity;        

        if (axis)
        {
            targetVelocity = transform.right.normalized;
            targetVelocity *= move * 10f;
        }
        else
        {
            targetVelocity = transform.up.normalized;
            targetVelocity *= move * 5f;
        }

        // Move the character by finding the target velocity
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        Vector2 lookDir = (mousePos - m_Rigidbody2D.position)*2.0f;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        m_Rigidbody2D.rotation = angle;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided " + collision.name);

    }
}
