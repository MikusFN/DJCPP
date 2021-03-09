using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;
    private int life = 50;
    private bool isAlive = true;


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
        if (isAlive == true && GetComponent<SpriteRenderer>().color.a < 1.0f)
        {
            GetComponent<SpriteRenderer>().color
                = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g,
                GetComponent<SpriteRenderer>().color.b, (GetComponent<SpriteRenderer>().color.a + Time.deltaTime));
        }
    }

    public void Move(float move, bool axis, float torqueValue)
    {
        Vector3 targetVelocity = m_Velocity;

        //Quaternion quat = Quaternion.LookRotation(m_Rigidbody2D.transform.up, m_Rigidbody2D.transform.forward);
        //quat.z += mousePos.x;

        //Vector2 lookDir = (mousePos - m_Rigidbody2D.position).normalized;

        //m_Rigidbody2D.transform.Rotate(new Vector3(0, 0, Mathf.Lerp(-90, 90,1)));

        //m_Rigidbody2D.AddForceAtPosition(transform.right.normalized * torqueValue*0.03f, 
        //    m_Rigidbody2D.position - new Vector2(m_Rigidbody2D.position.x, -GetComponent<SpriteRenderer>().bounds.size.y));

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

        //THIS COOULD BE USED FIR THE TELEPORT PICK UP
        //Vector2 lookDir = (mousePos - m_Rigidbody2D.position);
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //m_Rigidbody2D.rotation = angle;
    }

    public void TakeDamage(int damage)
    {
        if (life > damage)
        {
            life -= damage;
            GetComponent<SpriteRenderer>().color
                = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g,
                GetComponent<SpriteRenderer>().color.b, GetComponent<SpriteRenderer>().color.a * 0.1f);
            Debug.Log("Taken Damage");
        }
        else
        {
            life -= damage;
            isAlive = false;
            GetComponent<SpriteRenderer>().color
                = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g,
                GetComponent<SpriteRenderer>().color.b, GetComponent<SpriteRenderer>().color.a * 0.1f);
            Debug.Log("You Died");
        }
    }
}
