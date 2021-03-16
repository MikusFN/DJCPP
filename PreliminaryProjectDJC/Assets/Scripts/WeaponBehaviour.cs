using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float rateOfFire = 3.0f;

    private float timeOfLife = 0.0f;
    private List<Pickable> currentStatePickable;
    private float maxRateofFire = 0.3f;
    private int shotsFire = 1;
    private int maxShotsFire = 5;
    private PPUpType sidePP = PPUpType.none;

    // Start is called before the first frame update
    void Start()
    {
        currentStatePickable = new List<Pickable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (currentStatePickable.Count > 0)
            {
                foreach (var item in currentStatePickable)
                {
                    UsePPUP(item.PpType);
                }
                currentStatePickable.Clear();
            }
            Shoot();
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (currentStatePickable.Count > 0)
            {
                foreach (var item in currentStatePickable)
                {
                    UsePPUP(item.PpType);
                }
                currentStatePickable.Clear();
            }
            ShootPowerWeapon();
        }
    }

    void Shoot()
    {
        if (RateFire())
        {

            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            if (shotsFire > 1)
            {
                Instantiate(bulletPrefab, firePoint.position - new Vector3(.1f, 0, 0), firePoint.rotation);
                Instantiate(bulletPrefab, firePoint.position + new Vector3(.1f, 0, 0), firePoint.rotation);
            }
        }
    }

    void ShootPowerWeapon()
    {
        switch (sidePP)
        {
            case PPUpType.Teleport:
                //set new player position             
                PlayerMovement pm;
                TryGetComponent<PlayerMovement>(out pm);
                Transform player = GetComponentInParent<Transform>();
                if (player)
                {
                    player.position = pm.mainCam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
                }
                //instantiate the explosion and check for reached obstacles and enemies
                sidePP = PPUpType.none; //or a cold down timer 
                break;
            case PPUpType.destroyer:
                sidePP = PPUpType.none; //or a cold down timer 
                break;
            case PPUpType.none:
                //Put a sound or something
                break;
            default:
                break;
        }
    }

    private bool RateFire()
    {
        //contador de vida do projectil para o destroir 
        if (timeOfLife > rateOfFire)
        {
            timeOfLife = 0.0f;
            return true;
        }
        timeOfLife += Time.fixedDeltaTime;
        return false;
    }

    public void AddPickable(Pickable pickable)
    {
        currentStatePickable.Add(pickable);
    }

    public void UsePPUP(PPUpType pPUpType)
    {
        switch (pPUpType)
        {
            case PPUpType.RateOfFire:
                if (rateOfFire >= maxRateofFire)
                {
                    rateOfFire -= 0.3f;
                }
                break;
            case PPUpType.ShieldTime:
                //Up shield time or recover time
                break;
            case PPUpType.ShotsNum:
                if (shotsFire <= maxShotsFire)
                {
                    shotsFire += 2;
                }
                break;
            case PPUpType.Weapon:
                //Change weapon (create weopen class)
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
