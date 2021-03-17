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
    life,
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
            if (ppType == PPUpType.life || ppType == PPUpType.ShieldTime)
            {
                Debug.Log("have picked " + this.ppType);
            }
            else
            {
                player.GetComponent<WeaponBehaviour>().AddPickable(this);
                Debug.Log("have picked " + this.ppType);
            }
            GetComponent<CapsuleCollider2D>().isTrigger = true;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
