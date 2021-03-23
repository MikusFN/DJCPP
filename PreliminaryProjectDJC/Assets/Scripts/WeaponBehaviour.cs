using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponBehaviour : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject destroyerPrefab;
    public GameObject explosionAfPrefab;
    public GameObject DopplerPrefab;
    public float rateOfFire = 10;

    private float timeOfLife = 0.0f;
    private bool canFire;
    private List<Pickable> currentStatePickable;
    private float maxRateofFire = 0.1f;
    private int shotsFire = 1;
    private int maxShotsFire = 5;
    private PPUpType sidePP = PPUpType.none;

    public GameObject pickableIcon;
    public GameObject pickableText;


    // Start is called before the first frame update
    void Start()
    {
        currentStatePickable = new List<Pickable>();
        timeOfLife = rateOfFire;
        canFire = true;

        Debug.Log("Hola guapo");

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

    private void TeleportPreview()
    {
        PlayerMovement pm;

        if (TryGetComponent<PlayerMovement>(out pm))
        {
            Transform player = GetComponentInParent<Transform>();
            if (player)
            {
                //instantiate the explosion and check for reached obstacles and enemies
                Vector3 direction = (pm.mainCam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10)) - this.transform.position;
                int size = (int)(direction.magnitude / (Math.Sqrt(
                    (GetComponent<SpriteRenderer>().bounds.size.x * GetComponent<SpriteRenderer>().bounds.size.x)
                    +
                    (GetComponent<SpriteRenderer>().bounds.size.y * GetComponent<SpriteRenderer>().bounds.size.y))) + 1);

                for (int i = 0; i < size; i++)
                {
                    GameObject go1 = Instantiate(DopplerPrefab, this.transform.position + ((direction / size) * (i + 1)), this.transform.rotation);
                    go1.GetComponent<DopplerFade>().TimetoDestroy *= (i + 1);
                    //go1.GetComponent<DopplerFade>().Col.a = (i + 1);
                    GameObject go2 = Instantiate(DopplerPrefab, this.transform.position + ((direction / size) * (i + 0.66f)), this.transform.rotation);
                    go2.GetComponent<DopplerFade>().TimetoDestroy *= (i + 0.66f);
                    //go2.GetComponent<DopplerFade>().TimetoDestroy *= (i + 0.66f);
                    GameObject go3 = Instantiate(DopplerPrefab, this.transform.position + ((direction / size) * (i + 0.33f)), this.transform.rotation);
                    go3.GetComponent<DopplerFade>().TimetoDestroy *= (i + 0.33f);
                    //go3.GetComponent<DopplerFade>().TimetoDestroy *= (i + 0.33f);

                }
            }
        }
    }

    void TeleportNow()
    {
        PlayerMovement pm;

        if (TryGetComponent<PlayerMovement>(out pm))
        {
            TeleportPreview();
            Transform player = GetComponentInParent<Transform>();
            if (player)
            {
                //instantiate the explosion and check for reached obstacles and enemies
                GameObject go = Instantiate(explosionAfPrefab, firePoint.position, firePoint.rotation, this.transform);
                Vector3 auxPos = new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z);
                player.position = pm.mainCam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
                go.transform.position = auxPos;
            }
        }
    }

    void ShootPowerWeapon()
    {
        PlayerController pc;

        switch (sidePP)
        {
            case PPUpType.Teleport:
                TeleportNow();
                sidePP = PPUpType.none; //or a cold down timer 
                pickableIcon.GetComponent<PickableIcon>().emptyIcon();
                pickableText.GetComponent<Text>().text = "";
                break;
            case PPUpType.destroyer:
                if (TryGetComponent<PlayerController>(out pc))
                {
                    pc.ResetShield();
                }
                Instantiate(destroyerPrefab, firePoint.position, firePoint.rotation, this.transform);
                sidePP = PPUpType.none; //or a cold down timer 
                pickableIcon.GetComponent<PickableIcon>().emptyIcon();
                pickableText.GetComponent<Text>().text = "";
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

        Debug.Log("Pickable addeeeeeeeeed");
        GetComponent<PlayerController>().Score += (int)pickable.PpType;
        currentStatePickable.Add(pickable);

            switch (pickable.PpType)
            {
                case PPUpType.life:
                
                pickableIcon.GetComponent<PickableIcon>().ChangeIcon("life");
                pickableText.GetComponent<Text>().text = "+50 Life";


                break;

                case PPUpType.ShieldTime:

                pickableIcon.GetComponent<PickableIcon>().ChangeIcon("shield");
                pickableText.GetComponent<Text>().text = "Shield";


                break;

                case PPUpType.RateOfFire:

                pickableIcon.GetComponent<PickableIcon>().ChangeIcon("rateOfFire");
                pickableText.GetComponent<Text>().text = "Rate of Fire";

                break;

                case PPUpType.ShotsNum:

                pickableIcon.GetComponent<PickableIcon>().ChangeIcon("shotsNum");
                pickableText.GetComponent<Text>().text = "Shots Num";


                break;

                case PPUpType.Teleport:
                
                pickableIcon.GetComponent<PickableIcon>().ChangeIcon("teleport");
                pickableText.GetComponent<Text>().text = "Teleport";

                break;

                case PPUpType.destroyer:

                pickableIcon.GetComponent<PickableIcon>().ChangeIcon("destroyer");
                pickableText.GetComponent<Text>().text = "Destroyer";

                break;
            }
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
