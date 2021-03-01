using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{

    private float lenghtSprite, heightSprite, startPosX, startPosY, biggestSpriteLenght;

    public GameObject MainCamera;
    public GameObject background;
    public float effectStrenght;


    // Start is called before the first frame update
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        lenghtSprite = GetComponent<SpriteRenderer>().bounds.size.x;
        heightSprite = GetComponent<SpriteRenderer>().bounds.size.y;

        biggestSpriteLenght = 2.72f;
        //biggestSpriteLenght = GetBiggestSprite();
    }

    // Update is called once per frame
    void Update()
    {
        float temp = MainCamera.transform.position.x * (1 - effectStrenght);
        float dist = MainCamera.transform.position.x * effectStrenght;

        transform.position = new Vector3(startPosX + dist, startPosY, transform.position.z);

        if (Vector2.Distance(transform.position, MainCamera.transform.position) 
            > ((biggestSpriteLenght / 2) + (lenghtSprite / 2))
            && temp > startPosX + lenghtSprite)
        {
            startPosX += 2 * biggestSpriteLenght;
            startPosY = Random.Range(-(heightSprite / 2), (heightSprite / 2));
        }
    }
    private float GetBiggestSprite()
    {
        return background.GetComponent<SpriteBackgroundManager>().BiggestSpriteLenght;
    }

}
