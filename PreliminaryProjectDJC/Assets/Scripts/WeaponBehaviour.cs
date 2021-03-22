using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject destroyerPrefab;
    public GameObject explosionAfPrefab;
    public float rateOfFire = 10;

    private float timeOfLife = 0.0f;
    private bool canFire;
    private List<Pickable> currentStatePickable;
    private float maxRateofFire = 0.1f;
    private int shotsFire = 1;
    private int maxShotsFire = 5;
    private PPUpType sidePP = PPUpType.none;

    // Start is called before the first frame update
    void Start()
    {
        currentStatePickable = new List<Pickable>();
        timeOfLife = 0.0f;
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (currentStatePickable.Count > 0)
        {
            foreach (var item in currentStatePickable)
            {
                UsePPUP(item.PpType);
            }
            currentStatePickable.Clear();
        }
        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            RateFire();
            Shoot();
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            ShootPowerWeapon();
        }
    }
    void Shoot()
    {
        if (canFire)
        {

            GameObject go = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, this.transform);
            go.GetComponent<Rigidbody2D>().velocity = (go.transform.position - transform.position).normalized * go.GetComponent<Projectile>().speed;

            if (shotsFire > 1)
            {
                int numberShotsCount = (shotsFire - 1) / 2;
                for (int i = 0; i < numberShotsCount; i++)
                {
                    GameObject go2 = Instantiate(bulletPrefab, new Vector3((i + 1) * 0.01f, 0, 0) + firePoint.position, firePoint.rotation, this.transform);
                    go2.GetComponent<Rigidbody2D>().velocity = //((Quaternion.Euler(0, 0, (i + 1) * step)) * 
                         (go2.transform.position - transform.position).normalized * go.GetComponent<Projectile>().speed;

                    GameObject go3 = Instantiate(bulletPrefab, new Vector3(-(i + 1) * (0.01f), 0, 0) + firePoint.position, firePoint.rotation, this.transform);
                    go3.GetComponent<Rigidbody2D>().velocity = //((Quaternion.Euler(0, 0, (i + 1) * (-step))) * 
                         (go3.transform.position - transform.position).normalized * go.GetComponent<Projectile>().speed;
                }

            }
        }
    }

    void ShootPowerWeapon()
    {
        PlayerController pc;
        PlayerMovement pm;

        switch (sidePP)
        {
            case PPUpType.Teleport:
                //set new player position             
                TryGetComponent<PlayerMovement>(out pm);
                Transform player = GetComponentInParent<Transform>();
                if (player)
                {
                    //instantiate the explosion and check for reached obstacles and enemies
                    GameObject go = Instantiate(explosionAfPrefab, firePoint.position, firePoint.rotation, this.transform);
                    Vector3 auxPos =   new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z);
                    player.position = pm.mainCam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
                    go.transform.position = auxPos;

                }
                sidePP = PPUpType.none; //or a cold down timer 
                break;
            case PPUpType.destroyer:
                if (TryGetComponent<PlayerController>(out pc))
                {
                    pc.ResetShield();
                }
                Instantiate(destroyerPrefab, firePoint.position, firePoint.rotation, this.transform);
                sidePP = PPUpType.none; //or a cold down timer 
                break;
            case PPUpType.none:
                //Put a sound or something
                break;
            default:
                break;
        }
    }

    private void RateFire()
    {
        //contador de vida do projectil para o destroir 
        if (timeOfLife >= rateOfFire)
        {
            timeOfLife = 0.0f;
            canFire = true;
        }
        else
        {
            timeOfLife += Time.fixedDeltaTime;
            canFire = false;
        }
    }

    public void AddPickable(Pickable pickable)
    {
        GetComponent<PlayerController>().Score += (int)pickable.PpType;
        currentStatePickable.Add(pickable);
    }

    public void UsePPUP(PPUpType pPUpType)
    {
        PlayerController player;

        switch (pPUpType)
        {

            case PPUpType.life:
                if (TryGetComponent<PlayerController>(out player))
                {
                    player.AddLifeShield(pPUpType);
                }
                break;
            case PPUpType.RateOfFire:
                if (rateOfFire >= maxRateofFire)
                {
                    rateOfFire -= 0.1f;
                }
                break;
            case PPUpType.ShieldTime:
                if (TryGetComponent<PlayerController>(out player))
                {
                    player.AddLifeShield(pPUpType);
                }
                break;
            case PPUpType.ShotsNum:
                if (shotsFire <= maxShotsFire)
                {
                    shotsFire += 2;
                }
                break;
            case PPUpType.Teleport:
                sidePP = PPUpType.Teleport;
                break;
            case PPUpType.destroyer:
                sidePP = PPUpType.destroyer;
                break;
            default:
                break;
        }
    }
}
