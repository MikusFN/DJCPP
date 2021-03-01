using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBackgroundManager : MonoBehaviour
{
    public float BiggestSpriteLenght { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer[] sp = GetComponentsInChildren<SpriteRenderer>();

        if (sp.Length > 0)
        {
            for (int i = 0; i < sp.Length; i++)
            {
                if (BiggestSpriteLenght < sp[i].bounds.size.x)
                {
                    BiggestSpriteLenght = sp[i].bounds.size.x;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
