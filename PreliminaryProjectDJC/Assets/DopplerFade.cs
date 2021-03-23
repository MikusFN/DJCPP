using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DopplerFade : MonoBehaviour
{
    private float timetoDestroy = 10f;
    private float timeOfLife = 0.0f;
    private Color col;

    public float TimetoDestroy { get => timetoDestroy; set => timetoDestroy = value; }
    public Color Col { get => col; set => col = value; }

    // Start is called before the first frame update
    void Start()
    {
        Col = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        ////Color aux = new Color(col.r, col.r, col.b, col.a);
        //if ((int)timeOfLife % 5 == 0 && (int)timeOfLife > 1)
        //    col.a *= 0.9f;
        LifeTime();
    }

    private void LifeTime()
    {
        //contador de vida do projectil para o destroir 
        if (timeOfLife > timetoDestroy)
        {
            timeOfLife = 0.0f;
            Destroy(gameObject);
        }
        timeOfLife += Time.fixedDeltaTime;
    }
}
