using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PPUpType
{
    RateOfFire,
    ShotsNum,
    ShieldTime,
    Teleport,
    destroyer,
    none
}

public class Pickable : MonoBehaviour
{
    PPUpType ppType;
    public PPUpType PpType { get => ppType; set => ppType = value; }

    //public Pickable(bool powerUP)
    //{
    //    if (powerUP)
    //    {
    //        PpType = (PPUpType)Random.Range(0, 2);
    //        //PpType = (PPUpType)Random.Range(0, 4);
    //    }
    //    else
    //    {
    //        //PpType = (PPUpType)Random.Range(4, 6);
    //        PpType = (PPUpType)4;
    //    }
    //}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player;
        if (collision.TryGetComponent<PlayerController>(out player))
        {
            player.GetComponent<WeaponBehaviour>().AddPickable(this);
            GetComponent<CapsuleCollider2D>().isTrigger = true;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
