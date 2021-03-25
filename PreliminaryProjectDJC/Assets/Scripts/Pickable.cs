using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PPUpType
{
    life,
    ShieldTime,
    RateOfFire,
    ShotsNum,
    Teleport,
    destroyer,
    none
}

public class Pickable : MonoBehaviour
{
    PPUpType ppType;    

    public PPUpType PpType { get => ppType; set => ppType = value; }

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
