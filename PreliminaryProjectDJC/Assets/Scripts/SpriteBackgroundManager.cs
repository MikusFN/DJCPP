using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBackgroundManager : MonoBehaviour
{

    private float bheight = 0.0f, blenght = 0.0f;
    public float BiggestSpriteLenght 
    { get { return blenght; } }
    public float BiggestSpriteHeight
    { get{return bheight;} }

    // Start is called before the first frame update
    void Awake()
    {
        SpriteRenderer[] sp = GetComponentsInChildren<SpriteRenderer>();

        if (sp.Length > 0)
        {
            for (int i = 0; i < sp.Length; i++)
            {
                if (blenght < sp[i].bounds.size.x)
                {
                    blenght = sp[i].bounds.size.x;
                }
                if (bheight < sp[i].bounds.size.y)
                {
                    bheight = sp[i].bounds.size.y;
                }
            }
        }
    }    
}
