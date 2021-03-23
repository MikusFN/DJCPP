using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;
    private int life;
    private bool isAlive = true;
    private bool isShielding = true;
    private float shieldTime;
    private int score;
    //private SpriteRenderer shieldRenderer;

    public GameObject gameManager;
    public Text lifeUI;
    public Slider health;

    GameObject scoreTextUI;

    AudioSource audioDamage;

    //[Header("Events")]
    //[Space]
    //public UnityEvent OnLandEvent;

    public GameObject shield;
    public float velocityRB;
    public int maxLife = 500;
    public float maxShieldTime = 10.0f;

    public bool IsAlive { get => isAlive; set => isAlive = value; }
    public bool IsShielding { get => isShielding; set => isShielding = value; }
    public int Score { get => score; set => score = value; }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        velocityRB = m_Rigidbody2D.velocity.y;
        shieldTime = maxShieldTime;
        life = maxLife;
        Score = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        lifeUI.text = life.ToString();
        health.value = life;
        scoreTextUI = GameObject.FindGameObjectWithTag ("ScoreTextTag");

        shield.GetComponent<SpriteRenderer>().color =
            new Color(shield.GetComponent<SpriteRenderer>().color.r, shield.GetComponent<SpriteRenderer>().color.g, shield.GetComponent<SpriteRenderer>().color.b, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        isShielding = UpdateShield();
        if (!isShielding)
            if (IsAlive == true && GetComponent<SpriteRenderer>().color.a < 1.0f)
            {
                GetComponent<SpriteRenderer>().color
                    = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g,
                    GetComponent<SpriteRenderer>().color.b, (GetComponent<SpriteRenderer>().color.a + Time.deltaTime));
            }

        //Debug.Log("Score " + score);
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

    public bool UpdateShield()
    {
        if (isShielding)
        {
            shieldTime -= Time.deltaTime;
            shield.GetComponent<SpriteRenderer>().color =
            new Color(shield.GetComponent<SpriteRenderer>().color.r, shield.GetComponent<SpriteRenderer>().color.g, shield.GetComponent<SpriteRenderer>().color.b, (shieldTime / maxShieldTime) * 0.5f);
        }
        return shieldTime > 0.0f;
    }

    public void TakeDamage(int damage)
    {
        if (!isShielding)
            if (life > damage)
            {
                life -= damage;
                GetComponent<SpriteRenderer>().color
                    = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g,
                    GetComponent<SpriteRenderer>().color.b, GetComponent<SpriteRenderer>().color.a * 0.1f);
                //Debug.Log("Taken Damage");
				lifeUI.text = life.ToString();
            health.value = life;
            }
            else
            {
                life -= damage;
                IsAlive = false;
                GetComponent<SpriteRenderer>().color
                    = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g,
                    GetComponent<SpriteRenderer>().color.b, GetComponent<SpriteRenderer>().color.a * 0.1f);
                Debug.Log("You Died");
				lifeUI.text = life.ToString();
            health.value = life;
            gameManager.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
            }
    }

    public void AddLifeShield(PPUpType upType)
    {
        switch (upType)
        {
            case PPUpType.ShieldTime:
                if (shieldTime < maxShieldTime)
                //if ((shieldTime + 2) < maxShieldTime)
                //{
                //    shieldTime += 2;
                //}                    
                {
                    shieldTime = maxShieldTime;
                }
                Debug.Log("Shield " + shieldTime);
                break;
            case PPUpType.life:
                if (life < maxLife)
                    if ((life + 50) < maxLife)
                    {
                        life += 50;
                    }
                    else
                    {
                        life = maxLife;
                    }
                //Debug.Log("Life " + life);
                break;
            case PPUpType.none:
                break;
            default:
                break;
        }
    }

    public void ResetShield()
    {
        shieldTime = maxShieldTime;
    }
	
	public void ResetPlayer() {
        this.life = 100;
        isAlive = true;
        lifeUI.text = life.ToString();
        health.value = life;
    }

}

